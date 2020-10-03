using NUnit.Framework;
using KtxSharp;
using System.IO;

namespace Tests
{
	public class KtxWriterTests
	{
        [Test]
		public void ValidityWithValidSamplesTest()
		{
			// Arrange
			byte[] inputBytes1 = File.ReadAllBytes(CommonFiles.validSample1Filename);

            MemoryStream msWriter1 = new MemoryStream();

            // Act
            KtxStructure ktxStructure1 = null;
			using (MemoryStream msReader = new MemoryStream(inputBytes1))
			{
				ktxStructure1 = KtxLoader.LoadInput(msReader);
			}

            KtxWriter.WriteTo(ktxStructure1, msWriter1);

            // Assert
            CollectionAssert.AreEqual(inputBytes1, msWriter1.ToArray());
        }
    }
}