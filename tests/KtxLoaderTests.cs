using NUnit.Framework;
using KtxSharp;
using System.IO;
using System.Collections.Generic;

namespace Tests
{
	public class KtxLoaderTests
	{
		

		[Test]
		public void ValidityWithValidSamplesTest()
		{
			// Arrange
			byte[] inputBytes1 = File.ReadAllBytes(CommonFiles.validSample1Filename);
			byte[] inputBytes2 = File.ReadAllBytes(CommonFiles.validSample2Filename);
			byte[] inputBytes3 = File.ReadAllBytes(CommonFiles.validSample3Filename);
			byte[] inputBytes4 = File.ReadAllBytes(CommonFiles.validSample4Filename);
			byte[] inputBytes5 = File.ReadAllBytes(CommonFiles.validSample5Filename);
			byte[] inputBytes6 = File.ReadAllBytes(CommonFiles.validSample6Filename);
			byte[] inputBytes7 = File.ReadAllBytes(CommonFiles.validSample7Filename);
			byte[] inputBytes8 = File.ReadAllBytes(CommonFiles.validSample8Filename);

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

			bool wasTest5Valid = false;
			string test5PossibleError = "";
			using (MemoryStream ms5 = new MemoryStream(inputBytes5))
			{
				(wasTest5Valid, test5PossibleError) = KtxLoader.CheckIfInputIsValid(ms5);
			}

			bool wasTest6Valid = false;
			string test6PossibleError = "";
			using (MemoryStream ms6 = new MemoryStream(inputBytes6))
			{
				(wasTest6Valid, test6PossibleError) = KtxLoader.CheckIfInputIsValid(ms6);
			}

			bool wasTest7Valid = false;
			string test7PossibleError = "";
			using (MemoryStream ms7 = new MemoryStream(inputBytes7))
			{
				(wasTest7Valid, test7PossibleError) = KtxLoader.CheckIfInputIsValid(ms7);
			}

			bool wasTest8Valid = false;
			string test8PossibleError = "";
			using (MemoryStream ms8 = new MemoryStream(inputBytes8))
			{
				(wasTest8Valid, test8PossibleError) = KtxLoader.CheckIfInputIsValid(ms8);
			}

			// Assert
			var allFiles = new List<byte[]>() { inputBytes1, inputBytes2, inputBytes3, inputBytes4, inputBytes5, inputBytes6, inputBytes7, inputBytes8 };
			CollectionAssert.AllItemsAreUnique(allFiles, "All input should be unique");

			Assert.IsTrue(wasTest1Valid);
			Assert.IsTrue(wasTest2Valid);
			Assert.IsTrue(wasTest3Valid);
			Assert.IsTrue(wasTest4Valid);
			Assert.IsTrue(wasTest5Valid);
			Assert.IsTrue(wasTest6Valid);
			Assert.IsTrue(wasTest7Valid);
			Assert.IsTrue(wasTest8Valid);

			Assert.AreEqual("", test1PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test2PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test3PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test4PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test5PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test6PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test7PossibleError, "There should NOT be any errors");
			Assert.AreEqual("", test8PossibleError, "There should NOT be any errors");
		}

		[Test]
		public void ValidityWithInvalidSamplesTest()
		{
			// Arrange
			byte[] inputBytes4 = File.ReadAllBytes(CommonFiles.validSample4Filename);	

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
			byte[] inputBytes1 = File.ReadAllBytes(CommonFiles.validSample1Filename);
			byte[] inputBytes2 = File.ReadAllBytes(CommonFiles.validSample2Filename);
			byte[] inputBytes3 = File.ReadAllBytes(CommonFiles.validSample3Filename);
			byte[] inputBytes4 = File.ReadAllBytes(CommonFiles.validSample4Filename);
			byte[] inputBytes5 = File.ReadAllBytes(CommonFiles.validSample5Filename);
			byte[] inputBytes6 = File.ReadAllBytes(CommonFiles.validSample6Filename);
			byte[] inputBytes7 = File.ReadAllBytes(CommonFiles.validSample7Filename);
			byte[] inputBytes8 = File.ReadAllBytes(CommonFiles.validSample8Filename);

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

			KtxStructure ktxStructure5 = null;
			using (MemoryStream ms5 = new MemoryStream(inputBytes5))
			{
				ktxStructure5 = KtxLoader.LoadInput(ms5);
			}

			KtxStructure ktxStructure6 = null;
			using (MemoryStream ms6 = new MemoryStream(inputBytes6))
			{
				ktxStructure6 = KtxLoader.LoadInput(ms6);
			}

			KtxStructure ktxStructure7 = null;
			using (MemoryStream ms7 = new MemoryStream(inputBytes7))
			{
				ktxStructure7 = KtxLoader.LoadInput(ms7);
			}

			KtxStructure ktxStructure8 = null;
			using (MemoryStream ms8 = new MemoryStream(inputBytes8))
			{
				ktxStructure8 = KtxLoader.LoadInput(ms8);
			}

			// Assert
			var allFiles = new List<byte[]>() { inputBytes1, inputBytes2, inputBytes3, inputBytes4, inputBytes5, inputBytes6, inputBytes7, inputBytes8 };
			CollectionAssert.AllItemsAreUnique(allFiles, "All input should be unique");

			// Compressonator sample file resolution
			Assert.AreEqual(16, ktxStructure1.header.pixelWidth);
			Assert.AreEqual(16, ktxStructure1.header.pixelHeight);

			// Compressonator sample file format
			Assert.AreEqual(GlPixelFormat.GL_RGBA, ktxStructure1.header.glFormat);
			Assert.AreEqual((uint)GlPixelFormat.GL_RGBA, ktxStructure1.header.glFormatAsUint);
			Assert.IsTrue(ktxStructure1.header.isInputLittleEndian);

			// Compressonator sample file Data type and internal format
			Assert.AreEqual(GlDataType.GL_UNSIGNED_BYTE, ktxStructure1.header.glDataType);
			Assert.AreEqual((uint)GlDataType.GL_UNSIGNED_BYTE, ktxStructure1.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_RGBA8_OES, ktxStructure1.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_RGBA8_OES, ktxStructure1.header.glInternalFormatAsUint);
			

			// PVRTexTool sample file resolution
			Assert.AreEqual(16, ktxStructure2.header.pixelWidth);
			Assert.AreEqual(16, ktxStructure2.header.pixelHeight);

			// PVRTexTool sample file format
			Assert.IsTrue(ktxStructure2.header.isInputLittleEndian);

			// PVRTexTool sample file Data type and internal format
			Assert.AreEqual(GlDataType.GL_UNSIGNED_BYTE, ktxStructure2.header.glDataType);
			Assert.AreEqual((uint)GlDataType.GL_UNSIGNED_BYTE, ktxStructure2.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_RGBA8_OES, ktxStructure2.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_RGBA8_OES, ktxStructure2.header.glInternalFormatAsUint);


			// ETCPACK sample file resolution
			Assert.AreEqual(2048, ktxStructure3.header.pixelWidth);
			Assert.AreEqual(32, ktxStructure3.header.pixelHeight);

			// ETCPACK sample file Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure3.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure3.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_COMPRESSED_SIGNED_R11_EAC, ktxStructure3.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_COMPRESSED_SIGNED_R11_EAC, ktxStructure3.header.glInternalFormatAsUint);

			// ktx_specs.ktx resolution
			Assert.AreEqual(32, ktxStructure4.header.pixelWidth);
			Assert.AreEqual(32, ktxStructure4.header.pixelHeight);

			// ktx_specs.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure4.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure4.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure4.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure4.header.glInternalFormatAsUint);
			Assert.IsFalse(ktxStructure4.header.isInputLittleEndian);
			
			// ktx_specs.ktx Metadata
			Assert.AreEqual(1, ktxStructure4.header.metadataDictionary.Count);
			Assert.IsTrue(ktxStructure4.header.metadataDictionary.ContainsKey("api"));
			Assert.IsTrue(ktxStructure4.header.metadataDictionary["api"].isString);
			Assert.AreEqual("joke2", ktxStructure4.header.metadataDictionary["api"].stringValue);

			// etc2-rgba8.ktx resolution
			Assert.AreEqual(128, ktxStructure5.header.pixelWidth);
			Assert.AreEqual(128, ktxStructure5.header.pixelHeight);

			// etc2-rgba8.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure5.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure5.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_COMPRESSED_RGBA8_ETC2_EAC, ktxStructure5.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_COMPRESSED_RGBA8_ETC2_EAC, ktxStructure5.header.glInternalFormatAsUint);

			// smiling_etc_64x64_Compressonator.ktx resolution
			Assert.AreEqual(64, ktxStructure6.header.pixelWidth);
			Assert.AreEqual(64, ktxStructure6.header.pixelHeight);

			// smiling_etc_64x64_Compressonator.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure6.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure6.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure6.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_ETC1_RGB8_OES, ktxStructure6.header.glInternalFormatAsUint);

			// smiling_ATC_RGBA_Explicit.ktx resolution
			Assert.AreEqual(64, ktxStructure7.header.pixelWidth);
			Assert.AreEqual(64, ktxStructure7.header.pixelHeight);

			// smiling_ATC_RGBA_Explicit.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure7.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure7.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_ATC_RGBA_EXPLICIT_ALPHA_AMD, ktxStructure7.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_ATC_RGBA_EXPLICIT_ALPHA_AMD, ktxStructure7.header.glInternalFormatAsUint);

			// format_pvrtc1_4bpp_unorm.ktx resolution
			Assert.AreEqual(64, ktxStructure8.header.pixelWidth);
			Assert.AreEqual(64, ktxStructure8.header.pixelHeight);

			// format_pvrtc1_4bpp_unorm.ktx Data type and internal format
			Assert.AreEqual(GlDataType.Compressed, ktxStructure8.header.glDataType);
			Assert.AreEqual((uint)GlDataType.Compressed, ktxStructure8.header.glTypeAsUint);
			Assert.AreEqual(GlInternalFormat.GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG, ktxStructure8.header.glInternalFormat);
			Assert.AreEqual((uint)GlInternalFormat.GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG, ktxStructure8.header.glInternalFormatAsUint);
		}
	}
}