using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace KtxSharp
{
	/// <summary>
	/// KtxHeader class
	/// </summary>
	/// <remark>Based on specifications mentioned in https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/</remark>
	public sealed class KtxHeader
	{
		/// <summary>
		/// Is input little endian
		/// </summary>
		public readonly bool isInputLittleEndian;

		/// <summary>
		/// Endianness value
		/// </summary>
		public readonly uint endiannessValue;

		/// <summary>
		/// Is data uncompressed
		/// </summary>
		public readonly bool isUncompressed;

		/// <summary>
		/// GlType as uint
		/// </summary>
		public readonly uint glTypeAsUint;

		/// <summary>
		/// GlDataType
		/// </summary>
		public readonly GlDataType glDataType;
		
		/// <summary>
		/// GlType size as uint
		/// </summary>
		public readonly uint glTypeSizeAsUint;

		/// <summary>
		/// GlFormat as uint
		/// </summary>
		public readonly uint glFormatAsUint;

		/// <summary>
		/// GlInternalFormat as uint
		/// </summary>
		public readonly uint glInternalFormatAsUint;

		/// <summary>
		/// GlInternalFormat
		/// </summary>
		public readonly GlInternalFormat glInternalFormat;

		/// <summary>
		/// GlBaseInternal as uint
		/// </summary>
		public readonly uint glBaseInternalFormatAsUint;

		/// <summary>
		/// GlPixelFormat
		/// </summary>
		public readonly GlPixelFormat glPixelFormat;

		/// <summary>
		/// Width in pixels
		/// </summary>
		public readonly uint pixelWidth;

		/// <summary>
		/// Height in pixels
		/// </summary>
		public readonly uint pixelHeight;

		/// <summary>
		/// Depth in pixels
		/// </summary>
		public readonly uint pixelDepth;

		/// <summary>
		/// Number of array elements
		/// </summary>
		public readonly uint numberOfArrayElements;

		/// <summary>
		/// Number of faces
		/// </summary>
		public readonly uint numberOfFaces;

		/// <summary>
		/// Number of mipmap levels
		/// </summary>
		public readonly uint numberOfMipmapLevels;

		/// <summary>
		/// How many bytes of key value data there is
		/// </summary>
		public readonly uint bytesOfKeyValueData;

		/// <summary>
		/// Metadata dictionary (key is string)
		/// </summary>
		public readonly Dictionary<string, MetadataValue> metadataDictionary;

		/// <summary>
		/// KtxHeader constructor
		/// </summary>
		/// <param name="stream">Stream for reading</param>
		public KtxHeader(Stream stream)
		{
			// Skip first 12 bytes since they only contain identifier
			stream.Seek(12, SeekOrigin.Begin);

			// Read endianness as bytes
			byte[] endiannessBytes = new byte[4];
			int bytesRead = stream.Read(buffer: endiannessBytes, offset: 0, count: endiannessBytes.Length);

			if (bytesRead != 4)
			{
				throw new InvalidOperationException("Cannot read enough bytes from memory stream!");
			}

			if (!Common.littleEndianAsBytes.SequenceEqual(endiannessBytes) && !Common.bigEndianAsBytes.SequenceEqual(endiannessBytes))
			{
				throw new InvalidOperationException("Endianness info in header is not valid!");
			}

			this.isInputLittleEndian = Common.littleEndianAsBytes.SequenceEqual(endiannessBytes);

			// Turn endianness as bytes to uint
			this.endiannessValue = BitConverter.ToUInt32(endiannessBytes, 0);

			// See if following uint reads need endian swap
			bool shouldSwapEndianness = (this.endiannessValue != Common.expectedEndianValue);

			// Use the memory stream in a binary reader.
            using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
			{
				// Swap endianness for every KTX variable if needed
				
				this.glTypeAsUint = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
				if (GlDataType.IsDefined(typeof(GlDataType), this.glTypeAsUint))
				{
					this.glDataType = (GlDataType)this.glTypeAsUint;
				}
				else
				{
					this.glDataType = GlDataType.NotKnown;
				}

				this.glTypeSizeAsUint = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.glFormatAsUint = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.glInternalFormatAsUint = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
				if (GlInternalFormat.IsDefined(typeof(GlInternalFormat), this.glInternalFormatAsUint))
				{
					this.glInternalFormat = (GlInternalFormat)this.glInternalFormatAsUint;
				}
				else
				{
					this.glInternalFormat = GlInternalFormat.NotKnown;
				}

				this.glBaseInternalFormatAsUint = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
				if (GlPixelFormat.IsDefined(typeof(GlPixelFormat), this.glBaseInternalFormatAsUint))
				{
					this.glPixelFormat = (GlPixelFormat)this.glBaseInternalFormatAsUint;
				}
				else
				{
					this.glPixelFormat = GlPixelFormat.NotKnown;
				}

				this.pixelWidth = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.pixelHeight = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.pixelDepth = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.numberOfArrayElements = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.numberOfFaces = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.numberOfMipmapLevels = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();

				this.bytesOfKeyValueData = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
				
				// Check that bytesOfKeyValueData is mod 4
				if (this.bytesOfKeyValueData % 4 != 0)
				{
					throw new InvalidOperationException(ErrorGen.Modulo4Error(nameof(this.bytesOfKeyValueData), this.bytesOfKeyValueData));
				}

				this.metadataDictionary = ParseMetadata(reader.ReadBytes((int)this.bytesOfKeyValueData), shouldSwapEndianness);
			}
		}

		/// <summary>
		/// Write content to stream. Leaves stream open
		/// </summary>
		/// <param name="output">Output stream</param>
		public void WriteTo(Stream output)
		{
			using (BinaryWriter writer = new BinaryWriter(output, Encoding.UTF8, leaveOpen: true))
			{
				writer.Write(Common.expectedEndianValue);
				writer.Write(this.glTypeAsUint);
				writer.Write(this.glTypeSizeAsUint);
				writer.Write(this.glFormatAsUint);
				writer.Write(this.glInternalFormatAsUint);
				writer.Write(this.glBaseInternalFormatAsUint);
				writer.Write(this.pixelWidth);
				writer.Write(this.pixelHeight);
				writer.Write(this.pixelDepth);
				writer.Write(this.numberOfArrayElements);
				writer.Write(this.numberOfFaces);
				writer.Write(this.numberOfMipmapLevels);
				writer.Write(GetTotalSizeOfMetadata(this.metadataDictionary));
				foreach (var pair in this.metadataDictionary)
				{
					uint keyLenght = Common.GetLengthOfUtf8StringAsBytes(pair.Key) + 1;
					uint valueLength = pair.Value.GetSizeInBytes();
					uint totalLength = keyLenght + valueLength;
					writer.Write(totalLength);
					writer.Write(Common.GetUtf8StringAsBytes(pair.Key));
					writer.Write(Common.nulByte);
					writer.Write(pair.Value.GetAsBytes());
					if (pair.Value.isString)
					{
						writer.Write(Common.nulByte);
					}

					// Write padding if needed
					while (totalLength % 4 != 0)
					{
						writer.Write(Common.nulByte);
						totalLength++;
					}
				}
			}
		}

		#region Parse metadata

		private static Dictionary<string, MetadataValue> ParseMetadata(byte[] inputArray, bool shouldSwapEndianness)
		{
			Dictionary<string, MetadataValue> returnDictionary = new Dictionary<string, MetadataValue>();
			int position = 0;
			while (position < inputArray.Length)
			{
				uint combinedKeyAndValueSizeInBytes = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(BitConverter.ToUInt32(inputArray, position)) : BitConverter.ToUInt32(inputArray, position);

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

		#endregion // Parse metadata

		#region Write metadata

		private static uint GetTotalSizeOfMetadata(Dictionary<string, MetadataValue> pairs)
		{
			uint totalCount = 0;

			foreach (var pair in pairs)
			{
				totalCount += 4; // Size is always 4 bytes uint
				totalCount += Common.GetLengthOfUtf8StringAsBytes(pair.Key) + 1;
				totalCount += pair.Value.GetSizeInBytes();
			}

			// Add value padding if needed
			while (totalCount % 4 != 0)
			{
				totalCount++;
			}

			return totalCount;
		}

		#endregion // Write metadata


		#region ToString

		/// <summary>
		/// Print some info into string
		/// </summary>
		/// <returns>String</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine($"isInputLittleEndian: {isInputLittleEndian}");
			sb.AppendLine($"endiannessValue: {endiannessValue}");
			sb.AppendLine($"isUncompressed: {isUncompressed}");
			sb.AppendLine($"glTypeAsUint: {glTypeAsUint}");			
			sb.AppendLine($"glTypeSizeAsUint: {glTypeSizeAsUint}");
			sb.AppendLine($"glFormatAsUint: {glFormatAsUint}");
			sb.AppendLine($"glInternalFormatAsUint: {glInternalFormatAsUint}");
			sb.AppendLine($"glBaseInternalFormatAsUint: {glBaseInternalFormatAsUint}");
			sb.AppendLine($"pixelWidth: {pixelWidth}");
			sb.AppendLine($"pixelHeight: {pixelHeight}");
			sb.AppendLine($"pixelDepth: {pixelDepth}");
			sb.AppendLine($"numberOfArrayElements: {numberOfArrayElements}");
			sb.AppendLine($"numberOfFaces: {numberOfFaces}");
			sb.AppendLine($"numberOfMipmapLevels: {numberOfMipmapLevels}");
			sb.AppendLine($"bytesOfKeyValueData: {bytesOfKeyValueData}");

			return sb.ToString();
		} 

		#endregion // ToString
	}
}