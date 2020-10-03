using NUnit.Framework;
using KtxSharp;
using System;
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
			byte[] inputBytes2 = File.ReadAllBytes(CommonFiles.validSample2Filename);
			byte[] inputBytes3 = File.ReadAllBytes(CommonFiles.validSample3Filename);

			MemoryStream msWriter1 = new MemoryStream();
			MemoryStream msWriter2 = new MemoryStream();
			MemoryStream msWriter3 = new MemoryStream();

			// Act
			KtxStructure ktxStructure1 = null;
			KtxStructure ktxStructure2 = null;
			KtxStructure ktxStructure3 = null;

			using (MemoryStream msReader = new MemoryStream(inputBytes1))
			{
				ktxStructure1 = KtxLoader.LoadInput(msReader);
			}

			using (MemoryStream msReader = new MemoryStream(inputBytes2))
			{
				ktxStructure2 = KtxLoader.LoadInput(msReader);
			}

			using (MemoryStream msReader = new MemoryStream(inputBytes3))
			{
				ktxStructure3 = KtxLoader.LoadInput(msReader);
			}

			KtxWriter.WriteTo(ktxStructure1, msWriter1);
			KtxWriter.WriteTo(ktxStructure2, msWriter2);
			KtxWriter.WriteTo(ktxStructure3, msWriter3);

			// Assert
			CollectionAssert.AreEqual(inputBytes1, msWriter1.ToArray());
			CollectionAssert.AreEqual(inputBytes2, msWriter2.ToArray());
			CollectionAssert.AreEqual(inputBytes3, msWriter3.ToArray());
		}

		[Test]
		public void NullOrInvalidInputsTest()
		{
			// Arrange
			KtxStructure structure = null;
			
			MemoryStream msWriter = new MemoryStream();
			MemoryStream msWriterNonWriteable = new MemoryStream(new byte[] { 0 }, writable: false);

			// Act
			using (FileStream input = new FileStream(CommonFiles.validSample1Filename, FileMode.Open))
			{
				structure = KtxLoader.LoadInput(input);
			}

			// Assert
			Assert.Throws<NullReferenceException>(() => { KtxWriter.WriteTo(null, msWriter); });
			Assert.Throws<NullReferenceException>(() => { KtxWriter.WriteTo(structure, null); });
			Assert.Throws<ArgumentException>(() => { KtxWriter.WriteTo(structure, msWriterNonWriteable); });
		}
	}
}