using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace KtxSharp
{
	// Based on specifications mentioned in https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/
	public class KtxHeader
	{	
		public readonly bool isInputLittleEndian;
		public readonly uint endiannessValue;

		public readonly bool isUncompressed;
		public readonly uint glTypeAsUint;
		public readonly GlDataType glDataType;
		
		public readonly uint glTypeSizeAsUint;

		public readonly uint glFormatAsUint;

		public readonly uint glInternalFormatAsUint;
		public readonly GlInternalFormat glInternalFormat;

		public readonly uint glBaseInternalFormatAsUint;
		public readonly GlPixelFormat glPixelFormat;

		public readonly uint pixelWidth;
		public readonly uint pixelHeight;
		public readonly uint pixelDepth;
		public readonly uint numberOfArrayElements;
		public readonly uint numberOfFaces;
		public readonly uint numberOfMipmapLevels;
		public readonly uint bytesOfKeyValueData;

		public readonly Dictionary<string, MetadataValue> metadataDictionary;


		public KtxHeader(MemoryStream memoryStream)
		{
			// Skip first 12 bytes since they only contain identifier
			memoryStream.Seek(12, SeekOrigin.Begin);

			// Read endianness as bytes
			byte[] endiannessBytes = new byte[4];
			int bytesRead = memoryStream.Read(buffer: endiannessBytes, offset: 0, count: endiannessBytes.Length);

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
            using (BinaryReader reader = new BinaryReader(memoryStream, Encoding.UTF8, leaveOpen: true))
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


		#region ToString

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