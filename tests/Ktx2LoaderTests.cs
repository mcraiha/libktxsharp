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

		// Act
		Ktx2Structure ktx2Structure1 = null;
		using (MemoryStream ms1 = new MemoryStream(inputBytes1))
		{
			ktx2Structure1 = Ktx2Loader.LoadInput(ms1);
		}

		Ktx2Header ktx2Header1 = ktx2Structure1.ktx2Header;

		Ktx2Supercompression ktx2Supercompression1 = ktx2Structure1.ktx2Supercompression;

		Ktx2TextureData ktx2TextureData1 = ktx2Structure1.ktx2TextureData;

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
	}
}