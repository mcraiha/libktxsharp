using NUnit.Framework;
using KtxSharp;
using System.IO;
using System.Collections.Generic;

namespace Tests;

public class Ktx2LoaderTests
{
	[Test]
	public void CheckWithValidKtx2SamplesTest()
	{
		// Arrange
		byte[] inputBytes1 = Ktx2Samples.ktx2Sample1;
		byte[] inputBytes2 = File.ReadAllBytes(CommonFiles.validKtx2Sample1Filename);
		byte[] inputBytes3 = File.ReadAllBytes(CommonFiles.validKtx2Sample2Filename);

		// Act
		Ktx2Structure ktx2Structure1 = null;
		using (MemoryStream ms1 = new MemoryStream(inputBytes1))
		{
			ktx2Structure1 = Ktx2Loader.LoadInput(ms1);
		}

		Ktx2Header ktx2Header1 = ktx2Structure1.ktx2Header;

		Ktx2Supercompression ktx2Supercompression1 = ktx2Structure1.ktx2Supercompression;

		Ktx2TextureData ktx2TextureData1 = ktx2Structure1.ktx2TextureData;


		Ktx2Structure ktx2Structure2 = null;
		using (MemoryStream ms2 = new MemoryStream(inputBytes2))
		{
			ktx2Structure2 = Ktx2Loader.LoadInput(ms2);
		}

		Ktx2Header ktx2Header2 = ktx2Structure2.ktx2Header;

		Ktx2Supercompression ktx2Supercompression2 = ktx2Structure2.ktx2Supercompression;

		Ktx2TextureData ktx2TextureData2 = ktx2Structure2.ktx2TextureData;


		Ktx2Structure ktx2Structure3 = null;
		using (MemoryStream ms3 = new MemoryStream(inputBytes3))
		{
			ktx2Structure3 = Ktx2Loader.LoadInput(ms3);
		}

		Ktx2Header ktx2Header3 = ktx2Structure3.ktx2Header;

		Ktx2Supercompression ktx2Supercompression3 = ktx2Structure3.ktx2Supercompression;

		Ktx2TextureData ktx2TextureData3 = ktx2Structure3.ktx2TextureData;

		// Assert
		Assert.That(ktx2Header1.vkFormatUint, Is.EqualTo(0));
		Assert.That(ktx2Header1.vkFormat, Is.EqualTo(VkFormat.VK_FORMAT_UNDEFINED));
		Assert.That(ktx2Header1.typeSize, Is.EqualTo(1));
		Assert.That(ktx2Header1.pixelWidth, Is.EqualTo(8));
		Assert.That(ktx2Header1.pixelHeight, Is.EqualTo(8));
		Assert.That(ktx2Header1.pixelDepth, Is.EqualTo(0));
		Assert.That(ktx2Header1.supercompressionSchemeUint, Is.EqualTo(1));
		Assert.That(ktx2Header1.supercompressionScheme, Is.EqualTo(SupercompressionScheme.BasisLZ));

		Assert.That(ktx2Header1.dfdByteOffset, Is.EqualTo(0x00000068));
		Assert.That(ktx2Header1.dfdByteLength, Is.EqualTo(0x0000003C));

		Assert.That(ktx2Header1.kvdByteOffset, Is.EqualTo(0x000000C4));
		Assert.That(ktx2Header1.kvdByteLength, Is.EqualTo(0x00000058));

		Assert.That(ktx2Header1.sgdByteOffset, Is.EqualTo(0x0000000000000120));
		Assert.That(ktx2Header1.sgdByteLength, Is.EqualTo(0x0000000000000090));

		Assert.That(ktx2Header1.levelIndexes.Count, Is.EqualTo(1));
		Assert.That(ktx2Header1.levelIndexes[0].byteOffset, Is.EqualTo(0x00000000000001B0));
		Assert.That(ktx2Header1.levelIndexes[0].byteLength, Is.EqualTo(0x0000000000000003));
		Assert.That(ktx2Header1.levelIndexes[0].uncompressedByteLength, Is.EqualTo(0x0000000000000000));

		Assert.That(ktx2Header1.dfdTotalSize, Is.EqualTo(60));
		Assert.That(ktx2Header1.dataFormatDescriptorRaw.Length, Is.EqualTo(60 - Common.sizeOfUint));

		Assert.That(ktx2Header1.metadataDictionary.Count, Is.EqualTo(2));
		Assert.That(ktx2Header1.metadataDictionary.ContainsKey("KTXorientation"), Is.True);
		Assert.That(ktx2Header1.metadataDictionary["KTXorientation"].isString, Is.True);
		Assert.That(ktx2Header1.metadataDictionary["KTXorientation"].stringValue, Is.EqualTo("rd"));

		Assert.That(ktx2Header1.metadataDictionary.ContainsKey("KTXwriter"), Is.True);
		Assert.That(ktx2Header1.metadataDictionary["KTXwriter"].isString, Is.True);
		Assert.That(ktx2Header1.metadataDictionary["KTXwriter"].stringValue, Is.EqualTo("toktx v4.0.__default__ / libktx v4.0.__default__"));

		Assert.That(ktx2Header1.sgdByteLength, Is.EqualTo(ktx2Supercompression1.supercompressionGlobalDataRaw.Length));

		Assert.That(ktx2TextureData1.levelImages.Count, Is.EqualTo(1));
		Assert.That(ktx2Header1.levelIndexes[0].byteLength, Is.EqualTo(ktx2TextureData1.levelImages[0].Length));
		Assert.That(ktx2TextureData1.levelImages[0], Is.EqualTo(new byte[] {0x4E, 0x0E, 0x04}));


		Assert.That(ktx2Header2.vkFormatUint, Is.EqualTo(23));
		Assert.That(ktx2Header2.vkFormat, Is.EqualTo(VkFormat.VK_FORMAT_R8G8B8_UNORM));
		Assert.That(ktx2Header2.typeSize, Is.EqualTo(1));
		Assert.That(ktx2Header2.pixelWidth, Is.EqualTo(8));
		Assert.That(ktx2Header2.pixelHeight, Is.EqualTo(8));
		Assert.That(ktx2Header2.pixelDepth, Is.EqualTo(0));
		Assert.That(ktx2Header2.supercompressionSchemeUint, Is.EqualTo(0));
		Assert.That(ktx2Header2.supercompressionScheme, Is.EqualTo(SupercompressionScheme.None));

		Assert.That(ktx2Header2.dfdByteOffset, Is.EqualTo(0x00000068));
		Assert.That(ktx2Header2.dfdByteLength, Is.EqualTo(0x0000004C));

		Assert.That(ktx2Header2.kvdByteOffset, Is.EqualTo(0x000000B4));
		Assert.That(ktx2Header2.kvdByteLength, Is.EqualTo(0x00000030));

		Assert.That(ktx2Header2.sgdByteOffset, Is.EqualTo(0x0000000000000000));
		Assert.That(ktx2Header2.sgdByteLength, Is.EqualTo(0x0000000000000000));

		Assert.That(ktx2Header2.levelIndexes.Count, Is.EqualTo(1));
		Assert.That(ktx2Header2.levelIndexes[0].byteOffset, Is.EqualTo(0x00000000000000E4));
		Assert.That(ktx2Header2.levelIndexes[0].byteLength, Is.EqualTo(0x00000000000000C0));
		Assert.That(ktx2Header2.levelIndexes[0].uncompressedByteLength, Is.EqualTo(0x00000000000000C0));

		Assert.That(ktx2Header2.dfdTotalSize, Is.EqualTo(76));
		Assert.That(ktx2Header2.dataFormatDescriptorRaw.Length, Is.EqualTo(76 - Common.sizeOfUint));

		Assert.That(ktx2Header2.metadataDictionary.Count, Is.EqualTo(1));
		Assert.That(ktx2Header2.metadataDictionary.ContainsKey("KTXorientation"), Is.False);
		Assert.That(ktx2Header2.metadataDictionary.ContainsKey("KTXwriter"), Is.True);
		Assert.That(ktx2Header2.metadataDictionary["KTXwriter"].isString, Is.True);
		Assert.That(ktx2Header2.metadataDictionary["KTXwriter"].stringValue, Is.EqualTo("ktx create v4.4.2 / libktx v4.4.2"));

		Assert.That(ktx2Header2.sgdByteLength, Is.EqualTo(ktx2Supercompression2.supercompressionGlobalDataRaw.Length));

		Assert.That(ktx2TextureData2.levelImages.Count, Is.EqualTo(1));
		Assert.That(ktx2Header2.levelIndexes[0].byteLength, Is.EqualTo(ktx2TextureData2.levelImages[0].Length));
		Assert.That(ktx2TextureData2.levelImages[0], Is.EqualTo(new byte[] {0xFF, 0x00, 0x67, 0xE5, 0x01, 0x5D, 0xC2, 0x03, 0x51, 0xA3, 0x07, 0x46,
																			0x86, 0x0D, 0x3C, 0x6F, 0x14, 0x33, 0x58, 0x1E, 0x2B, 0x46, 0x2A, 0x24,
																			0xE2, 0x01, 0x5D, 0xC2, 0x03, 0x51, 0xA1, 0x07, 0x46, 0x85, 0x0D, 0x3C,
																			0x6D, 0x14, 0x33, 0x57, 0x1E, 0x2B, 0x45, 0x2A, 0x23, 0x34, 0x39, 0x1D,
																			0xC0, 0x03, 0x50, 0xA1, 0x07, 0x45, 0x86, 0x0D, 0x3B, 0x6D, 0x14, 0x33,
																			0x58, 0x1E, 0x2A, 0x44, 0x2B, 0x23, 0x34, 0x3A, 0x1D, 0x26, 0x4A, 0x17,
																			0xA1, 0x07, 0x45, 0x85, 0x0D, 0x3C, 0x6C, 0x15, 0x33, 0x56, 0x1F, 0x29,
																			0x44, 0x2A, 0x23, 0x34, 0x3A, 0x1D, 0x27, 0x4A, 0x16, 0x1B, 0x60, 0x11,
																			0x83, 0x0D, 0x3B, 0x6C, 0x15, 0x33, 0x56, 0x1F, 0x2A, 0x43, 0x2C, 0x23,
																			0x33, 0x3A, 0x1D, 0x25, 0x4A, 0x17, 0x1A, 0x5F, 0x11, 0x11, 0x77, 0x0D,
																			0x6C, 0x16, 0x32, 0x56, 0x20, 0x29, 0x43, 0x2C, 0x22, 0x33, 0x3B, 0x1C,
																			0x26, 0x4C, 0x17, 0x1A, 0x60, 0x11, 0x11, 0x76, 0x0D, 0x0A, 0x8E, 0x0A,
																			0x56, 0x1F, 0x29, 0x42, 0x2C, 0x23, 0x33, 0x3A, 0x1C, 0x25, 0x4C, 0x16,
																			0x1A, 0x60, 0x11, 0x11, 0x77, 0x0D, 0x0A, 0x90, 0x09, 0x05, 0xAF, 0x06,
																			0x42, 0x2C, 0x23, 0x33, 0x3B, 0x1C, 0x25, 0x4C, 0x17, 0x1A, 0x60, 0x11,
																			0x11, 0x77, 0x0D, 0x0A, 0x90, 0x09, 0x05, 0xAF, 0x06, 0x02, 0xCE, 0x04}));


		Assert.That(ktx2Header3.vkFormatUint, Is.EqualTo(23));
		Assert.That(ktx2Header3.vkFormat, Is.EqualTo(VkFormat.VK_FORMAT_R8G8B8_UNORM));
		Assert.That(ktx2Header3.typeSize, Is.EqualTo(1));
		Assert.That(ktx2Header3.pixelWidth, Is.EqualTo(8));
		Assert.That(ktx2Header3.pixelHeight, Is.EqualTo(8));
		Assert.That(ktx2Header3.pixelDepth, Is.EqualTo(0));
		Assert.That(ktx2Header3.supercompressionSchemeUint, Is.EqualTo(0));
		Assert.That(ktx2Header3.supercompressionScheme, Is.EqualTo(SupercompressionScheme.None));

		Assert.That(ktx2Header3.dfdByteOffset, Is.EqualTo(0x000000B0));
		Assert.That(ktx2Header3.dfdByteLength, Is.EqualTo(0x0000004C));

		Assert.That(ktx2Header3.kvdByteOffset, Is.EqualTo(0x000000FC));
		Assert.That(ktx2Header3.kvdByteLength, Is.EqualTo(0x00000030));

		Assert.That(ktx2Header3.sgdByteOffset, Is.EqualTo(0x0000000000000000));
		Assert.That(ktx2Header3.sgdByteLength, Is.EqualTo(0x0000000000000000));

		Assert.That(ktx2Header3.levelIndexes.Count, Is.EqualTo(4));
		Assert.That(ktx2Header3.levelIndexes[0].byteOffset, Is.EqualTo(0x0000000000000174));
		Assert.That(ktx2Header3.levelIndexes[0].byteLength, Is.EqualTo(0x00000000000000C0));
		Assert.That(ktx2Header3.levelIndexes[0].uncompressedByteLength, Is.EqualTo(0x00000000000000C0));
		Assert.That(ktx2Header3.levelIndexes[1].byteOffset, Is.EqualTo(0x0000000000000144));
		Assert.That(ktx2Header3.levelIndexes[1].byteLength, Is.EqualTo(0x0000000000000030));
		Assert.That(ktx2Header3.levelIndexes[1].uncompressedByteLength, Is.EqualTo(0x0000000000000030));
		Assert.That(ktx2Header3.levelIndexes[2].byteOffset, Is.EqualTo(0x0000000000000138));
		Assert.That(ktx2Header3.levelIndexes[2].byteLength, Is.EqualTo(0x000000000000000C));
		Assert.That(ktx2Header3.levelIndexes[2].uncompressedByteLength, Is.EqualTo(0x000000000000000C));
		Assert.That(ktx2Header3.levelIndexes[3].byteOffset, Is.EqualTo(0x000000000000012C));
		Assert.That(ktx2Header3.levelIndexes[3].byteLength, Is.EqualTo(0x0000000000000003));
		Assert.That(ktx2Header3.levelIndexes[3].uncompressedByteLength, Is.EqualTo(0x0000000000000003));

		Assert.That(ktx2Header3.dfdTotalSize, Is.EqualTo(76));
		Assert.That(ktx2Header3.dataFormatDescriptorRaw.Length, Is.EqualTo(76 - Common.sizeOfUint));

		Assert.That(ktx2Header3.metadataDictionary.Count, Is.EqualTo(1));
		Assert.That(ktx2Header3.metadataDictionary.ContainsKey("KTXorientation"), Is.False);
		Assert.That(ktx2Header3.metadataDictionary.ContainsKey("KTXwriter"), Is.True);
		Assert.That(ktx2Header3.metadataDictionary["KTXwriter"].isString, Is.True);
		Assert.That(ktx2Header3.metadataDictionary["KTXwriter"].stringValue, Is.EqualTo("ktx create v4.4.2 / libktx v4.4.2"));

		Assert.That(ktx2Header3.sgdByteLength, Is.EqualTo(ktx2Supercompression3.supercompressionGlobalDataRaw.Length));

		Assert.That(ktx2TextureData3.levelImages.Count, Is.EqualTo(4));
		Assert.That(ktx2Header3.levelIndexes[0].byteLength, Is.EqualTo(ktx2TextureData3.levelImages[3].Length));
		Assert.That(ktx2Header3.levelIndexes[1].byteLength, Is.EqualTo(ktx2TextureData3.levelImages[2].Length));
		Assert.That(ktx2Header3.levelIndexes[2].byteLength, Is.EqualTo(ktx2TextureData3.levelImages[1].Length));
		Assert.That(ktx2Header3.levelIndexes[3].byteLength, Is.EqualTo(ktx2TextureData3.levelImages[0].Length));
		Assert.That(ktx2TextureData3.levelImages[0], Is.EqualTo(new byte[] {0x52, 0x38, 0x27}));
		Assert.That(ktx2TextureData3.levelImages[1], Is.EqualTo(new byte[] {0x86, 0x18, 0x3B, 0x4E, 0x33, 0x26, 0x4D, 0x34, 0x25, 0x27, 0x61, 0x16}));
		Assert.That(ktx2TextureData3.levelImages[2], Is.EqualTo(new byte[] {0xCD, 0x09, 0x55, 0x9E, 0x0C, 0x44, 0x64, 0x1D, 0x2F, 0x4D, 0x32, 0x26,
																			0x9C, 0x0C, 0x44, 0x72, 0x11, 0x34, 0x43, 0x29, 0x23, 0x32, 0x43, 0x1B,
																			0x62, 0x1E, 0x2E, 0x42, 0x2B, 0x22, 0x20, 0x50, 0x15, 0x17, 0x72, 0x0F,
																			0x4B, 0x34, 0x25, 0x31, 0x44, 0x1B, 0x17, 0x73, 0x0F, 0x10, 0x9D, 0x0B}));
		Assert.That(ktx2TextureData3.levelImages[3], Is.EqualTo(new byte[] {0xFF, 0x00, 0x67, 0xE5, 0x01, 0x5D, 0xC2, 0x03, 0x51, 0xA3, 0x07, 0x46,
																			0x86, 0x0D, 0x3C, 0x6F, 0x14, 0x33, 0x58, 0x1E, 0x2B, 0x46, 0x2A, 0x24,
																			0xE2, 0x01, 0x5D, 0xC2, 0x03, 0x51, 0xA1, 0x07, 0x46, 0x85, 0x0D, 0x3C,
																			0x6D, 0x14, 0x33, 0x57, 0x1E, 0x2B, 0x45, 0x2A, 0x23, 0x34, 0x39, 0x1D,
																			0xC0, 0x03, 0x50, 0xA1, 0x07, 0x45, 0x86, 0x0D, 0x3B, 0x6D, 0x14, 0x33,
																			0x58, 0x1E, 0x2A, 0x44, 0x2B, 0x23, 0x34, 0x3A, 0x1D, 0x26, 0x4A, 0x17,
																			0xA1, 0x07, 0x45, 0x85, 0x0D, 0x3C, 0x6C, 0x15, 0x33, 0x56, 0x1F, 0x29,
																			0x44, 0x2A, 0x23, 0x34, 0x3A, 0x1D, 0x27, 0x4A, 0x16, 0x1B, 0x60, 0x11,
																			0x83, 0x0D, 0x3B, 0x6C, 0x15, 0x33, 0x56, 0x1F, 0x2A, 0x43, 0x2C, 0x23,
																			0x33, 0x3A, 0x1D, 0x25, 0x4A, 0x17, 0x1A, 0x5F, 0x11, 0x11, 0x77, 0x0D,
																			0x6C, 0x16, 0x32, 0x56, 0x20, 0x29, 0x43, 0x2C, 0x22, 0x33, 0x3B, 0x1C,
																			0x26, 0x4C, 0x17, 0x1A, 0x60, 0x11, 0x11, 0x76, 0x0D, 0x0A, 0x8E, 0x0A,
																			0x56, 0x1F, 0x29, 0x42, 0x2C, 0x23, 0x33, 0x3A, 0x1C, 0x25, 0x4C, 0x16,
																			0x1A, 0x60, 0x11, 0x11, 0x77, 0x0D, 0x0A, 0x90, 0x09, 0x05, 0xAF, 0x06,
																			0x42, 0x2C, 0x23, 0x33, 0x3B, 0x1C, 0x25, 0x4C, 0x17, 0x1A, 0x60, 0x11,
																			0x11, 0x77, 0x0D, 0x0A, 0x90, 0x09, 0x05, 0xAF, 0x06, 0x02, 0xCE, 0x04}));
	}
}