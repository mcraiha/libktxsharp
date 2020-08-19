using NUnit.Framework;
using KtxSharp;
using System.IO;

namespace Tests
{
	public class KtxLoaderTests
	{
		// If you add any more test files here, remember to add them to tests.csproj !

		/// <summary>
		/// First sample file is created with Compressonator https://github.com/GPUOpen-Tools/Compressonator
		/// </summary>
		private static readonly string validSample1Filename = "16x16_colors_Compressonator.ktx";

		/// <summary>
		/// Second sample file is created with PVRTexTool https://community.imgtec.com/developers/powervr/tools/pvrtextool/
		/// </summary>
		private static readonly string validSample2Filename = "16x16_colors_PVRTexTool.ktx";

		/// <summary>
		/// Third sample file is created with ETCPACK https://github.com/Ericsson/ETCPACK
		/// </summary>
		private static readonly string validSample3Filename = "testimage_SIGNED_R11_EAC.ktx";

		/// <summary>
		/// Fourth sample file is filled (missing parts filled with zeroes) from KTX specifications https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/#4
		/// </summary>
		private static readonly string validSample4Filename = "ktx_specs.ktx";

		[Test]
		public void ValidityWithValidSamplesTest()
		{
			// Arrange
			byte[] inputBytes1 = File.ReadAllBytes(validSample1Filename);
			byte[] inputBytes2 = File.ReadAllBytes(validSample2Filename);
			byte[] inputBytes3 = File.ReadAllBytes(validSample3Filename);
			byte[] inputBytes4 = File.ReadAllBytes(validSample4Filename);

			// Act
			bool wasTest1Valid = false;
			string test1PossibleError = "";
			using (MemoryStream ms1 = new MemoryStream(inputBytes1))
			{
				(wasTest1Valid, test1PossibleError) = KtxLoader.CheckIfInputIsValid(ms1);
			}

			bool wasTest2Valid = false;
			string test2PossibleError = "";
			using (MemoryStream ms2 = new MemoryStream(inputBytes2))
			{
				(wasTest2Valid, test2PossibleError) = KtxLoader.CheckIfInputIsValid(ms2);
			}

			bool wasTest3Valid = false;
			string test3PossibleError = "";
			using (MemoryStream ms3 = new MemoryStream(inputBytes3))
			{
				(wasTest3Valid, test3PossibleError) = KtxLoader.CheckIfInputIsValid(ms3);
			}

			bool wasTest4Valid = false;
			string test4PossibleError = "";
			using (MemoryStream ms4 = new MemoryStream(inputBytes4))
			{
				(wasTest4Valid, test4PossibleError) = KtxLoader.CheckIfInputIsValid(ms4);
			}

			// Assert
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes2, "Input files should NOT have equal content");
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes3, "Input files should NOT have equal content");
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes4, "Input files should NOT have equal content");

			Assert.IsTrue(wasTest1Valid);
			Assert.IsTrue(wasTest2Valid);
			Assert.IsTrue(wasTest3Valid);
			Assert.IsTrue(wasTest4Valid);

			Assert.AreEqual("", test1PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test2PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test3PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test4PossibleError, "There should NOT be any errors");
		}

		[Test]
		public void ValidityWithInvalidSamplesTest()
		{
			// Arrange
			byte[] inputBytes4 = File.ReadAllBytes(validSample4Filename);	

			// Act
			inputBytes4[73] = 0xC0; // Make string invalid UTF-8
			inputBytes4[74] = 0xB1;

			bool wasTest4Valid = false;
			string test4PossibleError = "";
			using (MemoryStream ms4 = new MemoryStream(inputBytes4))
			{
				(wasTest4Valid, test4PossibleError) = KtxLoader.CheckIfInputIsValid(ms4);
			}

			// Assert
			Assert.IsFalse(wasTest4Valid);
			Assert.IsTrue(test4PossibleError.Contains("Byte array to UTF-8 failed"));
		}

		[Test]
		public void CheckHeadersWithValidSamplesTest()
		{
			// Arrange
			byte[] inputBytes1 = File.ReadAllBytes(validSample1Filename);
			byte[] inputBytes2 = File.ReadAllBytes(validSample2Filename);
			byte[] inputBytes3 = File.ReadAllBytes(validSample3Filename);
			byte[] inputBytes4 = File.ReadAllBytes(validSample4Filename);

			// Act
			KtxStructure ktxStructure1 = null;
			using (MemoryStream ms1 = new MemoryStream(inputBytes1))
			{
				ktxStructure1 = KtxLoader.LoadInput(ms1);
			}

			KtxStructure ktxStructure2 = null;
			using (MemoryStream ms2 = new MemoryStream(inputBytes2))
			{
				ktxStructure2 = KtxLoader.LoadInput(ms2);
			}

			KtxStructure ktxStructure3 = null;
			using (MemoryStream ms3 = new MemoryStream(inputBytes3))
			{
				ktxStructure3 = KtxLoader.LoadInput(ms3);
			}

			KtxStructure ktxStructure4 = null;
			using (MemoryStream ms4 = new MemoryStream(inputBytes4))
			{
				ktxStructure4 = KtxLoader.LoadInput(ms4);
			}

			// Assert
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes2, "Input files should NOT have equal content");
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes3, "Input files should NOT have equal content");
			CollectionAssert.AreNotEqual(inputBytes1, inputBytes4, "Input files should NOT have equal content");

			// Compressonator sample file resolution
			Assert.AreEqual(16, ktxStructure1.header.pixelWidth);
			Assert.AreEqual(16, ktxStructure1.header.pixelHeight);
			
			// PVRTexTool sample file resolution
			Assert.AreEqual(16, ktxStructure2.header.pixelWidth);
			Assert.AreEqual(16, ktxStructure2.header.pixelHeight);

			// ETCPACK sample file resolution
			Assert.AreEqual(2048, ktxStructure3.header.pixelWidth);
			Assert.AreEqual(32, ktxStructure3.header.pixelHeight);

			// ktx_specs.ktx resolution
			Assert.AreEqual(32, ktxStructure4.header.pixelWidth);
			Assert.AreEqual(32, ktxStructure4.header.pixelHeight);

			// ktx_specs.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure4.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure4.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure4.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure4.header.glInternalFormatAsUint);
			
			// ktx_specs.ktx Metadata
			Assert.AreEqual(1, ktxStructure4.header.metadataDictionary.Count);
			Assert.IsTrue(ktxStructure4.header.metadataDictionary.ContainsKey("api"));
			Assert.IsTrue(ktxStructure4.header.metadataDictionary["api"].isString);
			Assert.AreEqual("joke2", ktxStructure4.header.metadataDictionary["api"].stringValue);
		}
	}
}