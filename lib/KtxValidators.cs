// Validators for headers and texture data
using System;
using System.IO;
using System.Text;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace KtxSharp;

/// <summary>
/// Static class for Ktx validation
/// </summary>
public static class KtxValidators
{
	// There must be at least 64 bytes of input
	private static readonly int minInputSizeInBytes = 64;

	/// <summary>
	/// Generic stream validation
	/// </summary>
	/// <param name="stream">Input stream to read</param>
	/// <returns>Tuple that tells if stream is valid, and possible error</returns>
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

		if (!stream.CanSeek)
		{
			return (isValid: false, possibleError: "Stream is not seekable!");
		}

		if (stream.Length < minInputSizeInBytes)
		{
			return (isValid: false, possibleError: $"KTX input should have at least { minInputSizeInBytes } bytes!");
		}

		return (isValid: true, possibleError: "");
	}

	/// <summary>
	/// Validate KTX identifier
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <returns>Tuple that tells if stream has valid identifier, and possible error</returns>
	public static (bool isValid, string possibleError) ValidateKtx1Identifier(Stream stream)
	{
		try
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
			{
				// TODO: Use ReadExactly when .NET 8 is support is dropped
				Span<byte> tempIdentifier = stackalloc byte[Common.ktx1ValidIdentifier.Length];
				_ = reader.Read(tempIdentifier);

				if (tempIdentifier.SequenceEqual(Common.ktx1ValidIdentifier.Span))
				{
					return (isValid: true, possibleError: "");
				}
				else
				{
					return (isValid: false, possibleError: "Identifier does not match requirements!");
				}
			}
		}
		catch (Exception e)
		{
			return (isValid: false, e.ToString());
		}
	}

	/// <summary>
	/// Validate KTX2 identifier
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <returns>Tuple that tells if stream has valid identifier, and possible error</returns>
	public static (bool isValid, string possibleError) ValidateKtx2Identifier(Stream stream)
	{
		try
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
			{
				// TODO: Use ReadExactly when .NET 8 is support is dropped
				Span<byte> tempIdentifier = stackalloc byte[Common.ktx2ValidIdentifier.Length];
				_ = reader.Read(tempIdentifier);

				if (tempIdentifier.SequenceEqual(Common.ktx2ValidIdentifier.Span))
				{
					return (isValid: true, possibleError: "");
				}
				else
				{
					return (isValid: false, possibleError: "Identifier does not match requirements!");
				}
			}
		}
		catch (Exception e)
		{
			return (isValid: false, e.ToString());
		}
	}

	/// <summary>
	/// Validate KTX header data
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <returns>Tuple that tells if stream is valid, and possible error</returns>
	public static (bool isValid, string possibleError) ValidateKtx1HeaderData(Stream stream)
	{
		// Use the stream in a binary reader.
		try
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
			{
				// Start validating header
				uint tempEndian = reader.ReadUInt32();

				if (Common.expectedEndianValue != tempEndian && Common.otherValidEndianValue != tempEndian)
				{
					return (isValid: false, possibleError: "Endianness does not match requirements!");
				}

				bool shouldSwapEndianness = (tempEndian != Common.expectedEndianValue);

				uint glTypeTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();
				// TODO: uint glType to enum

				// If glType is 0 it should mean that this is compressed texture
				bool assumeCompressedTexture = (glTypeTemp == 0);

				uint glTypeSizeTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				if (assumeCompressedTexture && glTypeSizeTemp != 1)
				{
					return (isValid: false, possibleError: "glTypeSize should be 1 for compressed textures!");
				}

				uint glFormatTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				if (assumeCompressedTexture && glFormatTemp != 0)
				{
					return (isValid: false, possibleError: "glFormat should be 0 for compressed textures!");
				}

				uint glInternalFormatTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint glBaseInternalFormatTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint pixelWidthTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint pixelHeightTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint pixelDepthTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint numberOfArrayElementsTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint numberOfFacesTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint numberOfMipmapLevelsTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();

				uint sizeOfKeyValueDataTemp = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();
				if (sizeOfKeyValueDataTemp % 4 != 0)
				{
					return (isValid: false, possibleError: ErrorGen.Modulo4Error(nameof(sizeOfKeyValueDataTemp), sizeOfKeyValueDataTemp));
				}
				
				// Validate metadata
				(bool validMedata, string possibleMetadataError) = ValidateKtx1Metadata(reader, sizeOfKeyValueDataTemp, shouldSwapEndianness);
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

	private const uint ktx2SmallestAllowedOffsetValue = 12 + 9*4 + 4*4 + 2*8;

	/// <summary>
	/// Validate KTX2 header data
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <returns>Tuple that tells if stream is valid, and possible error</returns>
	public static (bool isValid, string possibleError) ValidateKtx2HeaderData(Stream stream)
	{
		// Use the stream in a binary reader.
		try
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
			{
				// Start validating header

				uint vkFormatUint = reader.ReadUInt32();
				if (!VkFormat.IsDefined(typeof(VkFormat), vkFormatUint))
				{
					return (isValid: false, possibleError: $"Cannot turn {vkFormatUint} into VkFormat!");
				}

				VkFormat vkFormat = (VkFormat)vkFormatUint;

				if (Common.prohibitedFormats.Contains(vkFormat))
				{
					return (isValid: false, possibleError: $"The VkFormat {vkFormat} is in prohibited formats list!");
				}

				uint typeSize = reader.ReadUInt32();

				if (vkFormat == VkFormat.VK_FORMAT_UNDEFINED && typeSize != 1)
				{
					return (isValid: false, possibleError: $"VK_FORMAT_UNDEFINED and typeSize {typeSize} is not a valid combination!");
				}

				uint pixelWidth = reader.ReadUInt32();

				if (pixelWidth == 0)
				{
					return (isValid: false, possibleError: "PixelWidth cannot be 0!");
				}

				uint pixelHeight = reader.ReadUInt32();

				uint pixelDepth = reader.ReadUInt32();

				uint layerCount = reader.ReadUInt32();

				uint faceCount = reader.ReadUInt32();

				if (faceCount == 6 && pixelWidth != pixelHeight)
				{
					return (isValid: false, possibleError: $"PixelWidth and PixelHeight must be equal for cube textures!");
				}

				uint levelCount = reader.ReadUInt32();

				uint mipLoops = Math.Max(1, levelCount);

				uint supercompressionSchemeUint = reader.ReadUInt32();
				if (!SupercompressionScheme.IsDefined(typeof(SupercompressionScheme), supercompressionSchemeUint))
				{
					return (isValid: false, possibleError: $"Cannot turn {supercompressionSchemeUint} into SupercompressionScheme!");
				}


				// Index section

				uint earliestAllowedOffset = ktx2SmallestAllowedOffsetValue + mipLoops*8;

				uint dfdByteOffset = reader.ReadUInt32();

				if (dfdByteOffset < earliestAllowedOffset)
				{
					return (isValid: false, possibleError: $"dfdByteOffset {dfdByteOffset} is too small!");
				}

				uint dfdByteLength = reader.ReadUInt32();

				if (dfdByteLength == 0)
				{
					return (isValid: false, possibleError: "dfdByteLength cannot be 0!");
				}

				uint kvdByteOffset = reader.ReadUInt32();

				if (kvdByteOffset > 0 && kvdByteOffset < dfdByteOffset + dfdByteLength)
				{
					return (isValid: false, possibleError: $"kvdByteOffset {kvdByteOffset} cannot be smaller than dfdByteOffset + dfdByteLength {dfdByteOffset} + {dfdByteLength} = {dfdByteOffset + dfdByteLength}!");
				}

				uint kvdByteLength = reader.ReadUInt32();

				if (kvdByteLength == 0 && kvdByteOffset != 0)
				{
					return (isValid: false, possibleError: $"kvdByteLength can only be 0 if kvdByteOffset is also zero!");
				}

				ulong sgdByteOffset = reader.ReadUInt64();

				if (sgdByteOffset > 0 && sgdByteOffset < kvdByteOffset + kvdByteLength)
				{
					return (isValid: false, possibleError: $"sgdByteOffset {sgdByteOffset} cannot be smaller than kvdByteOffset + kvdByteLength {kvdByteOffset} + {kvdByteLength} = {kvdByteOffset + kvdByteLength}!");
				}

				ulong sgdByteLength = reader.ReadUInt64();

				if (sgdByteLength == 0 && sgdByteOffset != 0)
				{
					return (isValid: false, possibleError: $"sgdByteLength can only be 0 if sgdByteOffset is also zero!");
				}


				// Level Index
				ulong largestOffsetAndLength = Math.Max(Math.Max(dfdByteOffset + dfdByteLength, kvdByteOffset + kvdByteLength), sgdByteOffset + sgdByteLength);
				
				LevelIndex[] levelIndexes = new LevelIndex[mipLoops];
				for (uint u = 0; u < mipLoops; u++)
				{
					levelIndexes[(int)u] = new LevelIndex(reader.ReadUInt64(), reader.ReadUInt64(), reader.ReadUInt64());
					if (supercompressionSchemeUint == 0 && levelIndexes[(int)u].byteLength != levelIndexes[(int)u].uncompressedByteLength)
					{
						return (isValid: false, possibleError: $"Level index [{u}] has mismatch between byteLength and uncompressedByteLength!");
					}
					else if (supercompressionSchemeUint == 1 && levelIndexes[(int)u].uncompressedByteLength != 0)
					{
						return (isValid: false, possibleError: "When supercompressionScheme is BasisLZ, the uncompressedByteLength must be 0!");
					}
					else if (levelIndexes[(int)u].byteOffset < largestOffsetAndLength)
					{
						return (isValid: false, possibleError: $"Level index [{u}] has too small offset!");
					}
					else if (levelIndexes[(int)u].byteLength == 0)
					{
						return (isValid: false, possibleError: $"Level index [{u}] has byte length of 0!");
					}
				}


				// Data Format Descriptor

				uint dfdTotalSize = reader.ReadUInt32();

				_ = reader.ReadBytes((int)dfdTotalSize - Common.sizeOfUint);


				// Key/Value Data

				Dictionary<string, MetadataValue> metadataDictionary = Ktx2Metadata.ParseMetadata(reader.ReadBytes((int)kvdByteLength));

				// Some additional conditional padding
				if (sgdByteLength > 0)
				{
					int overFromAlign8 = (int)stream.Position % 8;
					if (overFromAlign8 > 0)
					{
						_ = reader.ReadBytes(8 - overFromAlign8);
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

	/// <summary>
	/// Validate texture data
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <param name="header">Header</param>
	/// <param name="expectedTextureDataSize">Expected texture data size</param>
	/// <returns>Tuple that tells if stream is valid, and possible error</returns>
	public static (bool isValid, string possibleError) ValidateKtx1TextureData(Stream stream, KtxHeader header, uint expectedTextureDataSize)
	{
		// Use the stream in a binary reader.
		try
		{
			using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
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
					uint imageSize = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();
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

	private static (bool isValid, string possibleError) ValidateKtx1Metadata(BinaryReader reader, uint bytesOfKeyValueData, bool shouldSwapEndianness)
	{
		uint currentPosition = 0;

		while (currentPosition < bytesOfKeyValueData)
		{
			uint combinedKeyAndValueSize = shouldSwapEndianness ? BinaryPrimitives.ReverseEndianness(reader.ReadUInt32()) : reader.ReadUInt32();
			currentPosition += (uint)Common.sizeOfUint;

			if ((currentPosition + combinedKeyAndValueSize) > bytesOfKeyValueData)
			{
				return (isValid: false, possibleError: "Metadata: combinedKeyAndValueSize would go beyond Metadata array!");
			}

			// There should be at least NUL
			Span<byte> keyAndValueAsBytes = reader.ReadBytes((int)combinedKeyAndValueSize);

			if (!keyAndValueAsBytes.Contains(Common.nulByte))
			{
				return (isValid: false, possibleError: "Metadata: KeyValue pair does not contain NUL byte!");
			}

			// Check if key is valid UTF-8 byte combination
			try
			{
				UTF8Encoding utf8ThrowException = new UTF8Encoding(false, throwOnInvalidBytes: true);
				_ = utf8ThrowException.GetCharCount(keyAndValueAsBytes);
			}
			catch (Exception e)
			{
				return (isValid: false, possibleError: $"Byte array to UTF-8 failed: {e}!");
			}

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