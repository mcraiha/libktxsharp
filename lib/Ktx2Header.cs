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
	/// specifies the size of the data type in bytes used to upload the data to a graphics API
	/// </summary>
	public readonly uint typeSize;

	/// <summary>
	/// The width of the texture image for level 0, in pixels.
	/// </summary>
	/// <remarks>0 is not a valid value!</remarks>
	public readonly uint pixelWidth;
	public readonly uint pixelHeight;
	public readonly uint pixelDepth;
	public readonly uint layerCount;
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
	/// The offset from the start of the file of the dfdTotalSize field of the Data Format Descriptor
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
	/// The offset from the start of the file of supercompressionGlobalData.
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

			this.dataFormatDescriptorRaw = reader.ReadBytes((int)this.dfdTotalSize);


			// Key/Value Data

			this.metadataDictionary = ParseMetadata(reader.ReadBytes((int)this.kvdByteLength));
		}
	}

	private static Dictionary<string, MetadataValue> ParseMetadata(byte[] inputArray)
	{
		Dictionary<string, MetadataValue> returnDictionary = new Dictionary<string, MetadataValue>();
		int position = 0;
		while (position < inputArray.Length)
		{
			uint combinedKeyAndValueSizeInBytes = BitConverter.ToUInt32(inputArray, position);

			// Pair must be larger than 0 bytes
			if (combinedKeyAndValueSizeInBytes == 0)
			{
				throw new InvalidOperationException("Metadata: combinedKeyAndValueSize cannot be 0!");
			}

			position += Common.sizeOfUint;

			// Error out in case size is larger than bytes left
			if (combinedKeyAndValueSizeInBytes + 4 > (uint) inputArray.Length)
			{
				throw new InvalidOperationException("Metadata: combinedKeyAndValueSize cannot be larger than whole metadata!");
			}

			// Find NUL since key should always have it
			int indexOfFirstNul = Array.IndexOf(inputArray, Common.nulByte, position);

			if (indexOfFirstNul < 0)
			{
				throw new InvalidOperationException("Metadata: No Nul found when looking for key");
			}

			int keyLength = indexOfFirstNul - position;

			if (keyLength > combinedKeyAndValueSizeInBytes)
			{
				throw new InvalidOperationException("Metadata: Key length is longer than combinedKeyAndValueSizeInBytes!");
			}

			string key = System.Text.Encoding.UTF8.GetString(bytes: inputArray, index: position, count: keyLength);
			
			position += (keyLength + 1 /* Because we have to skip nul byte*/);
			
			int valueLength = (int)combinedKeyAndValueSizeInBytes - keyLength;
			byte[] bytesOfValue = new byte[valueLength];
			Buffer.BlockCopy(src: inputArray, srcOffset: position, dst: bytesOfValue, dstOffset: 0, count: valueLength);

			returnDictionary[key] = new MetadataValue(bytesOfValue);

			position += valueLength;

			// Skip value paddings if there are any
			while (position % 4 != 0)
			{
				position++;
			}
		}

		return returnDictionary;
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