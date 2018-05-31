// Validators for headers and texture data
using System;
using System.IO;
using System.Text;
using System.Linq;

namespace KtxSharp
{
	public static class KtxValidators
	{
		// There must be at least 64 bytes of input
        private static readonly int minInputSizeInBytes = 64;

		public static (bool isValid, string possibleError) GenericStreamValidation(Stream stream)
		{
			if (stream == null)
			{
				return (isValid: false, possibleError: "Stream is null!");
			}

			if (!stream.CanRead)
            {
                return (isValid: false, possibleError: "Stream is not readable!");
            }

			if (stream.Length < minInputSizeInBytes)
            {
                return (isValid: false, possibleError: $"KTX input should have at least { minInputSizeInBytes } bytes!");
            }

			return (isValid: true, possibleError: "");
		}

		public static (bool isValid, string possibleError) ValidateHeaderData(MemoryStream memoryStream)
		{
			// Use the memory stream in a binary reader.
			try
			{
				using (BinaryReader reader = new BinaryReader(memoryStream, Encoding.UTF8, leaveOpen: true))
				{
					// Start validating header
					byte[] tempIdentifier = reader.ReadBytes(Common.onlyValidIdentifier.Length);

					if (!Common.onlyValidIdentifier.SequenceEqual(tempIdentifier))
					{
						return (isValid: false, possibleError: "Identifier does not match requirements!");
					}

					uint tempEndian = reader.ReadUInt32();

					if (Common.expectedEndianValue != tempEndian && Common.otherValidEndianValue != tempEndian)
					{
						return (isValid: false, possibleError: "Endianness does not match requirements!");
					}

					bool shouldSwapEndianness = (tempEndian != Common.expectedEndianValue);

					uint glTypeTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
					// TODO: uint glType to enum

					// If glType is 0 it should mean that this is compressed texture
					bool assumeCompressedTexture = (glTypeTemp == 0);

					uint glTypeSizeTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					if (assumeCompressedTexture && glTypeSizeTemp != 1)
					{
						return (isValid: false, possibleError: "glTypeSize should be 1 for compressed textures!");
					}

					uint glFormatTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					if (assumeCompressedTexture && glFormatTemp != 0)
					{
						return (isValid: false, possibleError: "glFormat should be 0 for compressed textures!");
					}

					uint glInternalFormatTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint glBaseInternalFormatTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint pixelWidthTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint pixelHeightTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint pixelDepthTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint numberOfArrayElementsTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint numberOfFacesTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint numberOfMipmapLevelsTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

					uint sizeOfKeyValueDataTemp = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
					if (sizeOfKeyValueDataTemp % 4 != 0)
					{
						return (isValid: false, possibleError: ErrorGen.Modulo4Error(nameof(sizeOfKeyValueDataTemp), sizeOfKeyValueDataTemp));
					}
					
					// Validate metadata
					(bool validMedata, string possibleMetadataError) = ValidateMetadata(reader, sizeOfKeyValueDataTemp, shouldSwapEndianness);
					if (!validMedata)
					{
						return (isValid: false, possibleError: possibleMetadataError);
					}				
				}
			}
			catch (Exception e)
			{
				return (isValid: false, e.ToString());
			}

			return (isValid: true, possibleError: "");
		}

		public static (bool isValid, string possibleError) ValidateTextureData(MemoryStream memoryStream, KtxHeader header, uint expectedTextureDataSize)
		{
			// Use the memory stream in a binary reader.
			try
			{
				using (BinaryReader reader = new BinaryReader(memoryStream, Encoding.UTF8, leaveOpen: true))
				{
					// Specs say that if value of certain things is zero (0) then it should be used as one (1) 
					uint mipmapLevels = (header.numberOfMipmapLevels == 0) ? 1 : header.numberOfMipmapLevels;

					uint numberOfArrayElements = (header.numberOfArrayElements == 0) ? 1 : header.numberOfArrayElements;

					uint pixelDepth = (header.pixelDepth == 0) ? 1 : header.pixelDepth;

					uint pixelHeight = (header.pixelHeight == 0) ? 1 : header.pixelHeight;

					uint totalLengthOfTextureDataSection = 0;

					// Check if length reads should be endian swapped
					bool shouldSwapEndianness = (header.endiannessValue != Common.expectedEndianValue);
					
					// Check each mipmap level separately
					for (uint u = 0; u < mipmapLevels; u++)
					{
						uint imageSize = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
						totalLengthOfTextureDataSection += (imageSize + (uint)Common.sizeOfUint);
						if (imageSize > expectedTextureDataSize || totalLengthOfTextureDataSection > expectedTextureDataSize)
						{
							return (isValid: false, "Texture data: More data than expected!");
						}

						// TODO: More checks!

						// Read but do not use data for anything
						reader.ReadBytes((int)imageSize);

						// Skip possible padding bytes
						while (imageSize % 4 != 0)
						{
							imageSize++;
							// Read but ignore values
							reader.ReadByte();
						}
					}
				}
			}
			catch (Exception e)
			{
				return (isValid: false, e.ToString());
			}
					
			return (isValid: true, possibleError: "");
		}

		private static (bool isValid, string possibleError) ValidateMetadata(BinaryReader reader, uint bytesOfKeyValueData, bool shouldSwapEndianness)
		{
			uint currentPosition = 0;

			while (currentPosition < bytesOfKeyValueData)
			{
				uint combinedKeyAndValueSize = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
				currentPosition += (uint)Common.sizeOfUint;

				if ((currentPosition + combinedKeyAndValueSize) > bytesOfKeyValueData)
				{
					return (isValid: false, possibleError: "Metadata: combinedKeyAndValueSize would go beyond Metadata array!");
				}

				// There should be at least NUL
				byte[] keyAndValueAsBytes = reader.ReadBytes((int)combinedKeyAndValueSize);

				if (!keyAndValueAsBytes.Contains(Common.nulByte))
				{
					return (isValid: false, possibleError: "Metadata: KeyValue pair does not contain NUL byte!");
				}

				// TODO: Check if key is valid UTF-8 byte combination

				currentPosition += combinedKeyAndValueSize;

				// Skip value paddings if there are any
				while (currentPosition % 4 != 0)
				{
					currentPosition++;
					reader.ReadByte();
				}
			}

			return (isValid: true, possibleError: "");
		}
	}
}