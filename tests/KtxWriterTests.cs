using NUnit.Framework;
using KtxSharp;
using System.IO;

namespace Tests
{
	public class KtxWriterTests
	{
		// If you add any more test files here, remember to add them to tests.csproj !

		/// <summary>
		/// First sample file is created with Compressonator https://github.com/GPUOpen-Tools/Compressonator
		/// </summary>
		private static readonly string validSample1Filename = "16x16_colors_Compressonator.ktx";

        [Test]
		public void ValidityWithValidSamplesTest()
		{
			// Arrange
			byte[] inputBytes1 = File.ReadAllBytes(validSample1Filename);

            MemoryStream msWriter = new MemoryStream();

            // Act
            KtxStructure ktxStructure1 = null;
			using (MemoryStream msReader = new MemoryStream(inputBytes1))
			{
				ktxStructure1 = KtxLoader.LoadInput(msReader);
			}

            KtxWriter.WriteTo(ktxStructure1, msWriter);

            // Assert
            CollectionAssert.AreEqual(inputBytes1, msWriter.ToArray());
        }
    }
}