using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Buffers.Binary;

namespace KtxSharp;

/// <summary>
/// Ktx2Header class
/// </summary>
/// <remark>Based on specifications mentioned in https://github.khronos.org/KTX-Specification/ktxspec.v2.html</remark>
public sealed class Ktx2Header
{
	// Basics

	/// <summary>
	/// Specifies the image format using Vulkan VkFormat enum values
	/// </summary>
	public readonly uint vkFormatUint;

	/// <summary>
	/// Specifies the size of the data type in bytes used to upload the data to a graphics API
	/// </summary>
	public readonly uint typeSize;

	/// <summary>
	/// The width of the texture image for level 0, in pixels.
	/// </summary>
	/// <remarks>0 is not a valid value!</remarks>
	public readonly uint pixelWidth;

	/// <summary>
	/// The height of the texture image for level 0, in pixels.
	/// </summary>
	/// <remarks>0 is not a valid value for block-compressed formats, including BasisLZ/ETC1S and UASTC</remarks>
	public readonly uint pixelHeight;

	/// <summary>
	/// The depth of the texture image for level 0
	/// </summary>
	/// <remarks>0 is not a valid value for block-compressed formats that have block depth greater than 1. Must be 0 for depth or stencil formats</remarks>
	public readonly uint pixelDepth;

	/// <summary>
	/// The number of array elements
	/// </summary>
	/// <remarks>If the texture is not an array texture, layerCount must equal 0</remarks>
	public readonly uint layerCount;

	/// <summary>
	/// Specifies the number of cubemap faces
	/// </summary>
	/// <remarks>For cubemaps and cubemap arrays this must be 6. For non cubemaps this must be 1</remarks>
	public readonly uint faceCount;

	/// <summary>
	/// The number of levels in the Mip Level Array
	/// </summary>
	/// <remarks>0 means that there is only one level and applications should generate other levels if needed. 1 means that there is only one level and no others are needed</remarks>
	public readonly uint levelCount;

	/// <summary>
	/// Indicates if a supercompression scheme has been applied to the data in levelImages
	/// </summary>
	public readonly uint supercompressionSchemeUint;


	// Index section

	/// <summary>
	/// The offset from the start of the file to the dfdTotalSize field of the Data Format Descriptor
	/// </summary>
	public readonly uint dfdByteOffset;

	/// <summary>
	/// The total number of bytes in the Data Format Descriptor including the dfdTotalSize field. dfdByteLength must equal dfdTotalSize
	/// </summary>
	public readonly uint dfdByteLength;

	/// <summary>
	/// The offset from the start of the file to key/value data
	/// </summary>
	public readonly uint kvdByteOffset;

	/// <summary>
	/// The total number of bytes of key/value data including all keyAndValueByteLength fields, all keyAndValue fields and all valuePadding fields
	/// </summary>
	public readonly uint kvdByteLength;

	/// <summary>
	/// The offset from the start of the file to supercompressionGlobalData.
	/// </summary>
	/// <remarks>The value must be 0 when sgdByteLength = 0</remarks>
	public readonly ulong sgdByteOffset;

	/// <summary>
	/// The number of bytes of supercompressionGlobalData
	/// </summary>
	public readonly ulong sgdByteLength;


	// Level Index

	/// <summary>
	/// Indexes for each mip levels
	/// </summary>
	/// <remarks>Zero index [0] has the largest mip</remarks>
	public readonly List<LevelIndex> levelIndexes = new List<LevelIndex>();

	// Data Format Descriptor

	/// <summary>
	/// Indicates the total number of bytes in the dfDescriptor including dfdTotalSize
	/// </summary>
	public readonly uint dfdTotalSize;

	/// <summary>
	/// Bytes of data format descriptor
	/// </summary>
	public readonly byte[] dataFormatDescriptorRaw;


	// Key/Value Data

	/// <summary>
	/// Metadata dictionary (key is string)
	/// </summary>
	public readonly Dictionary<string, MetadataValue> metadataDictionary = new Dictionary<string, MetadataValue>();


	// Custom enums

	/// <summary>
	/// Enum of vkFormatUint
	/// </summary>
	public readonly VkFormat vkFormat;

	/// <summary>
	/// Enum of supercompressionSchemeUint
	/// </summary>
	public readonly SupercompressionScheme supercompressionScheme;

	/// <summary>
	/// Type of the texture
	/// </summary>
	public readonly TextureTypeKtx2 textureType;

	/// <summary>
	/// Ktx2Header constructor
	/// </summary>
	/// <param name="stream">Stream for reading (must be seekable stream)</param>
	/// <param name="seekFromCurrent">Seek from current position (use this if your stream is not a single .ktx2 file)</param>
	public Ktx2Header(Stream stream, bool seekFromCurrent = false)
	{
		// Skip first 12 bytes since they only contain identifier (by default we assume that we are dealing with a single .ktx2 file)
		stream.Seek(12, seekFromCurrent ? SeekOrigin.Current : SeekOrigin.Begin);

		// Use the stream in a binary reader.
		using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
		{
			// Basics 

			this.vkFormatUint = reader.ReadUInt32();
			if (VkFormat.IsDefined(typeof(VkFormat), this.vkFormatUint))
			{
				this.vkFormat = (VkFormat)this.vkFormatUint;
			}
			else
			{
				this.vkFormat = VkFormat.ERROR;
			}

			this.typeSize = reader.ReadUInt32();

			this.pixelWidth = reader.ReadUInt32();

			this.pixelHeight = reader.ReadUInt32();

			this.pixelDepth = reader.ReadUInt32();

			this.layerCount = reader.ReadUInt32();

			this.faceCount = reader.ReadUInt32();

			this.levelCount = reader.ReadUInt32();

			this.textureType = Common.DetectKtx2Type(this.pixelWidth, this.pixelHeight, this.pixelDepth, this.layerCount, this.faceCount, this.levelCount);

			this.supercompressionSchemeUint = reader.ReadUInt32();
			if (SupercompressionScheme.IsDefined(typeof(SupercompressionScheme), this.supercompressionSchemeUint))
			{
				this.supercompressionScheme = (SupercompressionScheme)this.supercompressionSchemeUint;
			}
			else
			{
				this.supercompressionScheme = SupercompressionScheme.ERROR;
			}


			// Index section

			this.dfdByteOffset = reader.ReadUInt32();

			this.dfdByteLength = reader.ReadUInt32();

			this.kvdByteOffset = reader.ReadUInt32();

			this.kvdByteLength = reader.ReadUInt32();

			this.sgdByteOffset = reader.ReadUInt64();

			this.sgdByteLength = reader.ReadUInt64();


			// Level Index

			uint mipLoops = Math.Max(1, this.levelCount);
			for (uint u = 0; u < mipLoops; u++)
			{
				this.levelIndexes.Add(new LevelIndex(reader.ReadUInt64(), reader.ReadUInt64(), reader.ReadUInt64()));
			}


			// Data Format Descriptor

			this.dfdTotalSize = reader.ReadUInt32();

			this.dataFormatDescriptorRaw = reader.ReadBytes((int)this.dfdTotalSize - Common.sizeOfUint);


			// Key/Value Data

			this.metadataDictionary = Ktx2Metadata.ParseMetadata(reader.ReadBytes((int)this.kvdByteLength));

			// Some additional conditional padding
			if (this.sgdByteLength > 0)
			{
				int overFromAlign8 = (int)stream.Position % 8;
				if (overFromAlign8 > 0)
				{
					_ = reader.ReadBytes(8 - overFromAlign8);
				}
			}
		}
	}
}

/// <summary>
/// 
/// </summary>
public readonly struct LevelIndex
{
	/// <summary>
	/// Offset from the start of the file
	/// </summary>
	public readonly ulong byteOffset;

	/// <summary>
	/// Compressed length in bytes
	/// </summary>
	public readonly ulong byteLength;

	/// <summary>
	/// Uncompressed length in bytes
	/// </summary>
	public readonly ulong uncompressedByteLength;

	/// <summary>
	/// Only constructor
	/// </summary>
	/// <param name="offset">Byte offset</param>
	/// <param name="compressedLength">Compressed byte length</param>
	/// <param name="uncompressedLength">Uncompressed byte length</param>
	public LevelIndex(ulong offset, ulong compressedLength, ulong uncompressedLength)
	{
		this.byteOffset = offset;
		this.byteLength = compressedLength;
		this.uncompressedByteLength = uncompressedLength;
	}
}