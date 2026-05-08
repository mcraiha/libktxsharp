using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Buffers.Binary;

namespace KtxSharp;

/// <summary>
/// Ktx2View class
/// </summary>
/// <remark>
/// Does not store the data, but acts as view over existing data.
/// Based on specifications mentioned in https://github.khronos.org/KTX-Specification/ktxspec.v2.html
/// </remark>
public sealed class Ktx2View
{
	private readonly Memory<byte> data;

	/// <summary>
	/// Only constructor
	/// </summary>
	/// <param name="input">Memory that contains Ktx2 data as bytes</param>
	/// <param name="doSafetyChecks">Do safety checks during construction? (default: false)</param>
	public Ktx2View(Memory<byte> input, bool doSafetyChecks = false)
	{
		this.data = input;

		if (doSafetyChecks)
		{
			if (!this.data.Slice(0, Common.ktx2ValidIdentifier.Length).Span.SequenceEqual(Common.ktx2ValidIdentifier.Span))
			{
				throw new InvalidDataException("Invalid Ktx2 identifier");
			}


			// TODO: Add more of these
		}
	}

	/// <summary>
	/// Get image format as Uint value
	/// </summary>
	/// <returns>Uint</returns>
	public uint GetVkFormatUint()
	{
		return BitConverter.ToUInt32(this.data.Slice(12).Span);
	}

	/// <summary>
	/// Get image format as Vulkan VkFormat enum
	/// </summary>
	/// <returns>VkFormat</returns>
	public VkFormat GetVkFormat()
	{
		uint value = this.GetVkFormatUint();
		if (VkFormat.IsDefined(typeof(VkFormat), value))
		{
			return (VkFormat)value;
		}

		return VkFormat.ERROR;
	}

	/// <summary>
	/// Get size of the data type in bytes used to upload the data to a graphics API
	/// </summary>
	/// <returns>Uint</returns>
	public uint GetTypeSize()
	{
		return BitConverter.ToUInt32(this.data.Slice(16).Span);
	}

	/// <summary>
	/// The width of the texture image for level 0, in pixels.
	/// </summary>
	/// <returns>Pixel width</returns>
	public uint GetPixelWidth()
	{
		return BitConverter.ToUInt32(this.data.Slice(20).Span);
	}

	/// <summary>
	/// The height of the texture image for level 0, in pixels.
	/// </summary>
	/// <returns>Pixel height</returns>
	public uint GetPixelHeight()
	{
		return BitConverter.ToUInt32(this.data.Slice(24).Span);
	}

	/// <summary>
	/// The depth of the texture image for level 0
	/// </summary>
	/// <returns></returns>
	public uint GetPixelDepth()
	{
		return BitConverter.ToUInt32(this.data.Slice(28).Span);
	}

	/// <summary>
	/// The number of array elements
	/// </summary>
	/// <returns></returns>
	public uint GetLayerCount()
	{
		return BitConverter.ToUInt32(this.data.Slice(32).Span);
	}

	/// <summary>
	/// Specifies the number of cubemap faces
	/// </summary>
	/// <returns></returns>
	public uint GetFaceCount()
	{
		return BitConverter.ToUInt32(this.data.Slice(36).Span);
	}

	/// <summary>
	/// The number of levels in the Mip Level Array
	/// </summary>
	/// <returns></returns>
	/// <remarks>0 means that there is only one level and applications should generate other levels if needed. 1 means that there is only one level and no others are needed</remarks>
	public uint GetLevelCount()
	{
		return BitConverter.ToUInt32(this.data.Slice(40).Span);
	}

	/// <summary>
	/// Supercompression scheme as Uint
	/// </summary>
	/// <returns>Uint</returns>
	public uint GetSupercompressionSchemeUint()
	{
		return BitConverter.ToUInt32(this.data.Slice(44).Span);
	}

	/// <summary>
	/// Supercompression scheme
	/// </summary>
	/// <returns>SupercompressionScheme</returns>
	public SupercompressionScheme GetSupercompressionScheme()
	{
		uint value = this.GetSupercompressionSchemeUint();

		if (SupercompressionScheme.IsDefined(typeof(SupercompressionScheme), value))
		{
			return (SupercompressionScheme)value;
		}

		return SupercompressionScheme.ERROR;
	}

	/// <summary>
	/// The offset from the start of the file to the dfdTotalSize field of the Data Format Descriptor
	/// </summary>
	/// <returns></returns>
	public uint GetDfdByteOffset()
	{
		return BitConverter.ToUInt32(this.data.Slice(48).Span);
	}

	/// <summary>
	/// The total number of bytes in the Data Format Descriptor including the dfdTotalSize field. dfdByteLength must equal dfdTotalSize
	/// </summary>
	/// <returns></returns>
	public uint GetDfdByteLength()
	{
		return BitConverter.ToUInt32(this.data.Slice(52).Span);
	}

	/// <summary>
	/// The offset from the start of the file to key/value data
	/// </summary>
	/// <returns></returns>
	public uint GetKvdByteOffset()
	{
		return BitConverter.ToUInt32(this.data.Slice(56).Span);
	}

	/// <summary>
	/// The total number of bytes of key/value data including all keyAndValueByteLength fields, all keyAndValue fields and all valuePadding fields
	/// </summary>
	/// <returns></returns>
	public uint GetKvdByteLength()
	{
		return BitConverter.ToUInt32(this.data.Slice(60).Span);
	}

	/// <summary>
	/// The offset from the start of the file to supercompressionGlobalData
	/// </summary>
	/// <returns></returns>
	/// <remarks>The value must be 0 when sgdByteLength = 0</remarks>
	public ulong GetSgdByteOffset()
	{
		return BitConverter.ToUInt64(this.data.Slice(64).Span);
	}

	/// <summary>
	/// The number of bytes of supercompressionGlobalData
	/// </summary>
	/// <returns></returns>
	public ulong GetSgdByteLength()
	{
		return BitConverter.ToUInt64(this.data.Slice(72).Span);
	}

	/// <summary>
	/// Indexes for each mip levels
	/// </summary>
	/// <returns></returns>
	/// <remarks>Zero index [0] has the largest mip</remarks>
	public List<LevelIndex> GetLevelIndexes()
	{
		uint mipLoops = Math.Max(1, this.GetLayerCount());
		List<LevelIndex> returnValues = new List<LevelIndex>((int)mipLoops);

		int startDataIndex = 80;
		for (uint u = 0; u < mipLoops; u++)
		{
			ulong firstValue = BitConverter.ToUInt64(this.data.Slice(startDataIndex).Span);
			ulong secondValue = BitConverter.ToUInt64(this.data.Slice(startDataIndex+8).Span);
			ulong thirdValue = BitConverter.ToUInt64(this.data.Slice(startDataIndex+16).Span);
			returnValues.Add(new LevelIndex(firstValue, secondValue, thirdValue));

			startDataIndex += 24;
		}

		return returnValues;
	}

	/// <summary>
	/// Indicates the total number of bytes in the dfDescriptor including dfdTotalSize
	/// </summary>
	/// <returns></returns>
	public uint GetDfdTotalSize()
	{
		int startDataIndex = (int)this.GetDfdByteOffset();
		return BitConverter.ToUInt32(this.data.Slice(startDataIndex).Span);
	}

	/// <summary>
	/// Bytes of data format descriptor
	/// </summary>
	/// <returns></returns>
	public Memory<byte> GetDataFormatDescriptorRaw()
	{
		int startDataIndex = (int)this.GetDfdByteOffset() + Common.sizeOfUint;
		int totalSize = (int)this.GetDfdTotalSize() - Common.sizeOfUint;
		return this.data.Slice(startDataIndex, totalSize);
	}

	/// <summary>
	/// Metadata dictionary (key is string)
	/// </summary>
	/// <returns></returns>
	public Dictionary<string, MetadataValue> GetMetadataDictionary()
	{
		int startDataIndex = (int)this.GetKvdByteOffset();
		int totalSize = (int)this.GetKvdByteLength();
		return Ktx2Metadata.ParseMetadata(this.data.Slice(startDataIndex, totalSize).Span);
	}

	/// <summary>
	/// Get bytes of supercompression global data
	/// </summary>
	/// <returns></returns>
	public Memory<byte> GetSupercompressionGlobalDataRaw()
	{
		int startDataIndex = (int)this.GetSgdByteOffset();
		int totalSize = (int)this.GetSgdByteLength();
		return this.data.Slice(startDataIndex, totalSize);
	}

	/// <summary>
	/// Get all bytes of certain mip map level
	/// </summary>
	/// <param name="levelIndex">Zero based index</param>
	/// <returns>all bytes of certain mip map level</returns>
	/// <remarks>Zero index [0] has the smallest mip (order is reverse if compared to GetLevelIndexes())</remarks>
	public Memory<byte> GetLevelImage(int levelIndex)
	{
		var orderedList = this.GetLevelIndexes();
		orderedList.Sort((a,b) => a.byteOffset.CompareTo(b.byteOffset)); // Sort the copy

		int startDataIndex = (int)orderedList[levelIndex].byteOffset;
		int totalSize = (int)orderedList[levelIndex].byteLength;
		return this.data.Slice(startDataIndex, totalSize);
	}
}