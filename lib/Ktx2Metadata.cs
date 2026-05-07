using System;
using System.Collections.Generic;

namespace KtxSharp;

/// <summary>
/// Static class for Ktx2 metadata parsing
/// </summary>
public static class Ktx2Metadata
{
	/// <summary>
	/// Parse metadata
	/// </summary>
	/// <param name="inputArray">Input bytes</param>
	/// <returns>Dictionary with key + value pairs</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static Dictionary<string, MetadataValue> ParseMetadata(ReadOnlySpan<byte> inputArray)
	{
		Dictionary<string, MetadataValue> returnDictionary = new Dictionary<string, MetadataValue>();
		int position = 0;
		while (position < inputArray.Length)
		{
			uint combinedKeyAndValueSizeInBytes = BitConverter.ToUInt32(inputArray.Slice(position));

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
			int indexOfFirstNul = inputArray.Slice(position).IndexOf(Common.nulByte);

			if (indexOfFirstNul < 0)
			{
				throw new InvalidOperationException("Metadata: No Nul found when looking for key");
			}

			int keyLength = indexOfFirstNul;

			if (keyLength > combinedKeyAndValueSizeInBytes)
			{
				throw new InvalidOperationException("Metadata: Key length is longer than combinedKeyAndValueSizeInBytes!");
			}

			string key = System.Text.Encoding.UTF8.GetString(inputArray.Slice(position, keyLength));
			
			position += (keyLength + 1 /* Because we have to skip nul byte*/);
			
			int valueLength = (int)combinedKeyAndValueSizeInBytes - keyLength - 1;
			byte[] bytesOfValue = new byte[valueLength];
			inputArray.Slice(position, valueLength).CopyTo(bytesOfValue);

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