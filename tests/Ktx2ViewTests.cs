using NUnit.Framework;
using KtxSharp;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Tests;

public class Ktx2ViewTests
{
	[Test]
	public void CheckWithValidKtx2SamplesTest()
	{
		// Arrange
		byte[] inputBytes1 = Ktx2Samples.ktx2Sample1;
		byte[] inputBytes2 = File.ReadAllBytes(CommonFiles.validKtx2Sample1Filename);

		// Act
		Ktx2View view1 = new Ktx2View(inputBytes1, doSafetyChecks: true);
		List<LevelIndex> levelIndexes1 = view1.GetLevelIndexes();
		Dictionary<string, MetadataValue> metadataDictionary1 = view1.GetMetadataDictionary();
		Memory<byte> supercompressonGlobalDataRaw1 = view1.GetSupercompressionGlobalDataRaw();

		Ktx2View view2 = new Ktx2View(inputBytes2, doSafetyChecks: true);
		List<LevelIndex> levelIndexes2 = view2.GetLevelIndexes();
		Dictionary<string, MetadataValue> metadataDictionary2 = view2.GetMetadataDictionary();
		Memory<byte> supercompressonGlobalDataRaw2 = view2.GetSupercompressionGlobalDataRaw();

		// Assert
		Assert.That(view1.GetVkFormatUint(), Is.EqualTo(0));
		Assert.That(view1.GetVkFormat(), Is.EqualTo(VkFormat.VK_FORMAT_UNDEFINED));
		Assert.That(view1.GetTypeSize(), Is.EqualTo(1));
		Assert.That(view1.GetPixelWidth(), Is.EqualTo(8));
		Assert.That(view1.GetPixelHeight(), Is.EqualTo(8));
		Assert.That(view1.GetPixelDepth(), Is.EqualTo(0));
		Assert.That(view1.GetSupercompressionSchemeUint(), Is.EqualTo(1));
		Assert.That(view1.GetSupercompressionScheme(), Is.EqualTo(SupercompressionScheme.BasisLZ));

		Assert.That(view1.GetDfdByteOffset(), Is.EqualTo(0x00000068));
		Assert.That(view1.GetDfdByteLength(), Is.EqualTo(0x0000003C));

		Assert.That(view1.GetKvdByteOffset(), Is.EqualTo(0x000000A4));
		Assert.That(view1.GetKvdByteLength(), Is.EqualTo(0x00000058));

		Assert.That(view1.GetSgdByteOffset(), Is.EqualTo(0x0000000000000100));
		Assert.That(view1.GetSgdByteLength(), Is.EqualTo(0x000000000000008C));

		Assert.That(levelIndexes1.Count, Is.EqualTo(1));
		Assert.That(levelIndexes1[0].byteOffset, Is.EqualTo(0x000000000000018C));
		Assert.That(levelIndexes1[0].byteLength, Is.EqualTo(0x0000000000000003));
		Assert.That(levelIndexes1[0].uncompressedByteLength, Is.EqualTo(0x0000000000000000));

		Assert.That(view1.GetDfdTotalSize(), Is.EqualTo(60));
		Assert.That(view1.GetDataFormatDescriptorRaw().Length, Is.EqualTo(60 - Common.sizeOfUint));

		Assert.That(metadataDictionary1.Count, Is.EqualTo(2));
		Assert.That(metadataDictionary1.ContainsKey("KTXorientation"), Is.True);
		Assert.That(metadataDictionary1["KTXorientation"].isString, Is.True);
		Assert.That(metadataDictionary1["KTXorientation"].stringValue, Is.EqualTo("rd"));

		Assert.That(metadataDictionary1.ContainsKey("KTXwriter"), Is.True);
		Assert.That(metadataDictionary1["KTXwriter"].isString, Is.True);
		Assert.That(metadataDictionary1["KTXwriter"].stringValue, Is.EqualTo("toktx v4.0.__default__ / libktx v4.0.__default__"));

		Assert.That(view1.GetSgdByteLength(), Is.EqualTo(supercompressonGlobalDataRaw1.Length));

		Assert.That(levelIndexes1[0].byteLength, Is.EqualTo(view1.GetLevelImage(0).Length));
		Assert.That(view1.GetLevelImage(0).ToArray(), Is.EqualTo(new byte[] {0x4E, 0x0E, 0x04}));


		Assert.That(view2.GetVkFormatUint, Is.EqualTo(23));
		Assert.That(view2.GetVkFormat, Is.EqualTo(VkFormat.VK_FORMAT_R8G8B8_UNORM));
		Assert.That(view2.GetTypeSize(), Is.EqualTo(1));
		Assert.That(view2.GetPixelWidth(), Is.EqualTo(8));
		Assert.That(view2.GetPixelHeight(), Is.EqualTo(8));
		Assert.That(view2.GetPixelDepth(), Is.EqualTo(0));
		Assert.That(view2.GetSupercompressionSchemeUint(), Is.EqualTo(0));
		Assert.That(view2.GetSupercompressionScheme(), Is.EqualTo(SupercompressionScheme.None));

		Assert.That(view2.GetDfdByteOffset(), Is.EqualTo(0x00000068));
		Assert.That(view2.GetDfdByteLength(), Is.EqualTo(0x0000004C));

		Assert.That(view2.GetKvdByteOffset(), Is.EqualTo(0x000000B4));
		Assert.That(view2.GetKvdByteLength(), Is.EqualTo(0x00000030));

		Assert.That(view2.GetSgdByteOffset(), Is.EqualTo(0x0000000000000000));
		Assert.That(view2.GetSgdByteLength(), Is.EqualTo(0x0000000000000000));

		Assert.That(levelIndexes2.Count, Is.EqualTo(1));
		Assert.That(levelIndexes2[0].byteOffset, Is.EqualTo(0x00000000000000E4));
		Assert.That(levelIndexes2[0].byteLength, Is.EqualTo(0x00000000000000C0));
		Assert.That(levelIndexes2[0].uncompressedByteLength, Is.EqualTo(0x00000000000000C0));

		Assert.That(view2.GetDfdTotalSize(), Is.EqualTo(76));
		Assert.That(view2.GetDataFormatDescriptorRaw().Length, Is.EqualTo(76 - Common.sizeOfUint));

		Assert.That(metadataDictionary2.Count, Is.EqualTo(1));
		Assert.That(metadataDictionary2.ContainsKey("KTXorientation"), Is.False);
		Assert.That(metadataDictionary2.ContainsKey("KTXwriter"), Is.True);
		Assert.That(metadataDictionary2["KTXwriter"].isString, Is.True);
		Assert.That(metadataDictionary2["KTXwriter"].stringValue, Is.EqualTo("ktx create v4.4.2 / libktx v4.4.2"));

		Assert.That(view2.GetSgdByteLength(), Is.EqualTo(supercompressonGlobalDataRaw2.Length));

		Assert.That(levelIndexes2[0].byteLength, Is.EqualTo(view2.GetLevelImage(0).Length));
		Assert.That(view2.GetLevelImage(0).ToArray(), Is.EqualTo(new byte[] {0xFF, 0x00, 0x67, 0xE5, 0x01, 0x5D, 0xC2, 0x03, 0x51, 0xA3, 0x07, 0x46,
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