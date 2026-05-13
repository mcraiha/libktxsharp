using NUnit.Framework;
using KtxSharp;
using System;
using System.IO;
using System.Linq;

namespace Tests;

public class KtxValidatorsTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void GenericStreamValidationTest()
	{
		// Arrange
		MemoryStream ms = new MemoryStream(new byte[] { 0 });
		// Close MemoryStream since then it should have CanRead as false https://msdn.microsoft.com/en-us/library/system.io.memorystream.canread.aspx
		ms.Close();

		MemoryStream notMuchContent = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

		// Act
		var nullShouldError = KtxValidators.GenericStreamValidation(null);
		var closedMemoryStreamShouldError = KtxValidators.GenericStreamValidation(ms);
		var notEnoughContentShouldError = KtxValidators.GenericStreamValidation(notMuchContent);

		// Assert
		Assert.IsFalse(nullShouldError.isValid);
		Assert.IsTrue(nullShouldError.possibleError.Contains("is null"));

		Assert.IsFalse(closedMemoryStreamShouldError.isValid);
		Assert.IsTrue(closedMemoryStreamShouldError.possibleError.Contains("not readable"));

		Assert.IsFalse(notEnoughContentShouldError.isValid);
		Assert.IsTrue(notEnoughContentShouldError.possibleError.Contains("should have at"));
	}

	[Test]
	public void KtxHeaderGenerationValidation()
	{
		// Arrange
		KtxHeader header = new KtxHeader(GlDataType.Compressed, GlPixelFormat.GL_RGBA, GlInternalFormat.GL_COMPRESSED_RGBA_ASTC_10x10_KHR, 256, 256, 1, new System.Collections.Generic.Dictionary<string, MetadataValue>());

		// Act
		MemoryStream ms1 = new MemoryStream();
		header.WriteTo(ms1);
		MemoryStream ms2 = new MemoryStream(ms1.ToArray());
		(bool valid, string possibleError) = KtxValidators.ValidateKtx1HeaderData(ms2);

		// Assert
		Assert.IsTrue(valid);
		Assert.IsTrue(string.IsNullOrEmpty(possibleError));
	}

	[Test]
	public void ValidateKtx2HeaderDataInvalidInputs()
	{
		// Arrange
		MemoryStream msIncorrectVkFormat = new MemoryStream(new byte[] { 0xFF, 0xFF, 0xFF, 0xFE });
		MemoryStream msProhibitedVkFormat = new MemoryStream(BitConverter.GetBytes((uint)Common.prohibitedFormats.First()));
		MemoryStream msTypesizeMismatch = new MemoryStream(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
		MemoryStream msPixelWidthZero = new MemoryStream(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
		MemoryStream msPixelWidthHeightCubeMismatch = new MemoryStream(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00 });

		// Act
		var errorIncorrectVkFormat = KtxValidators.ValidateKtx2HeaderData(msIncorrectVkFormat);
		var errorProhibitedVkFormat = KtxValidators.ValidateKtx2HeaderData(msProhibitedVkFormat);
		var errorTypesizeMismatch = KtxValidators.ValidateKtx2HeaderData(msTypesizeMismatch);
		var errorPixelWidthZero = KtxValidators.ValidateKtx2HeaderData(msPixelWidthZero);
		var errorPixelWidthHeightCubeMismatch = KtxValidators.ValidateKtx2HeaderData(msPixelWidthHeightCubeMismatch);

		// Assert
		Assert.That(errorIncorrectVkFormat.isValid, Is.False);
		Assert.That(errorIncorrectVkFormat.possibleError.Contains("into VkFormat!"), Is.True);

		Assert.That(errorProhibitedVkFormat.isValid, Is.False);
		Assert.That(errorProhibitedVkFormat.possibleError, Is.EqualTo($"The VkFormat {Common.prohibitedFormats.First()} is in prohibited formats list!"));

		Assert.That(errorTypesizeMismatch.isValid, Is.False);
		Assert.That(errorTypesizeMismatch.possibleError, Is.EqualTo("VK_FORMAT_UNDEFINED and typeSize 0 is not a valid combination!"));

		Assert.That(errorPixelWidthZero.isValid, Is.False);
		Assert.That(errorPixelWidthZero.possibleError, Is.EqualTo("PixelWidth cannot be 0!"));

		Assert.That(errorPixelWidthHeightCubeMismatch.isValid, Is.False);
		Assert.That(errorPixelWidthHeightCubeMismatch.possibleError, Is.EqualTo("PixelWidth and PixelHeight must be equal for cube textures!"));
	}
}
