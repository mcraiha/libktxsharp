using NUnit.Framework;
using KtxSharp;
using System.IO;

namespace Tests
{
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
			(bool valid, string possibleError) = KtxValidators.ValidateHeaderData(ms2);

			// Assert
			Assert.IsTrue(valid);
			Assert.IsTrue(string.IsNullOrEmpty(possibleError));
		}

		[Test, Description("KTX2 identifier test support")]
		public void Ktx2IdentifierTest()
		{
			// Arrange
			byte[] bytes = new byte[] 
			{ 
				// Sample data from https://github.khronos.org/KTX-Specification/ktxspec.v2.html
				0xAB, 0x4B, 0x54, 0x58, // first four bytes of Byte[12] identifier
				0x20, 0x32, 0x30, 0xBB, // next four bytes of Byte[12] identifier
				0x0D, 0x0A, 0x1A, 0x0A, // final four bytes of Byte[12] identifier
				0x00, 0x00, 0x00, 0x00, // UInt32 vkFormat = VK_FORMAT_UNDEFINED (0)
				0x01, 0x00, 0x00, 0x00, // UInt32 typeSize = 1
				0x08, 0x00, 0x00, 0x00, // UInt32 pixelWidth = 8
				0x08, 0x00, 0x00, 0x00, // UInt32 pixelHeight = 8
				0x00, 0x00, 0x00, 0x00, // UInt32 pixelDepth = 0
				0x00, 0x00, 0x00, 0x00, // UInt32 layerCount = 0
				0x01, 0x00, 0x00, 0x00, // UInt32 faceCount = 0
				0x01, 0x00, 0x00, 0x00, // UInt32 levelCount = 0
				0x01, 0x00, 0x00, 0x00, // UInt32 supercompressionScheme = 1 (BASISLZ)
			};

			MemoryStream ktx2Header = new MemoryStream(bytes);

			// Act
			var kt2ContentError = KtxValidators.ValidateIdentifier(ktx2Header);

			// Assert
			Assert.IsFalse(kt2ContentError.isValid);
			Assert.IsTrue(kt2ContentError.possibleError.Contains("KTX version 2"));
		}
	}
}