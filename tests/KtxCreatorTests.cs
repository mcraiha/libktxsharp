using NUnit.Framework;
using KtxSharp;
using System.IO;
using System.Collections.Generic;

namespace Tests
{
	public class KtxCreatorTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void KtxCreatorValidInputTest()
		{
			// Arange
			byte[] textureLevel0 = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }; // Not a valid ATSC block most likely!
			List<byte[]> textureDatas = new List<byte[]>() { textureLevel0 };

			// Act

			// Assert
			Assert.DoesNotThrow(() => KtxCreator.Create(GlDataType.Compressed, GlPixelFormat.GL_RGBA, GlInternalFormat.GL_COMPRESSED_RGBA_ASTC_8x8_KHR, 8, 8, textureDatas, new System.Collections.Generic.Dictionary<string, MetadataValue>()));
		}
	}
}