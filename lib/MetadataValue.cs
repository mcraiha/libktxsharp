// MetadataValue class
using System;

namespace KtxSharp
{
	/// <summary>
	/// Metadata value class
	/// </summary>
	public sealed class MetadataValue
	{
		/// <summary>
		/// Is string (false means byte array)
		/// </summary>
		public readonly bool isString;

		/// <summary>
		/// Bytes value, null if metadata is strings
		/// </summary>
		public readonly byte[] bytesValue;

		/// <summary>
		/// String value, null if metadata is byte array
		/// </summary>
		public readonly string stringValue;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="input">Input bytes</param>
		public MetadataValue(byte[] input)
		{
			int indexOfNull = Array.FindIndex(input, b => b == Common.nulByte);
			if (indexOfNull > -1)
			{
				// Basically if input array contains any NUL byte, it means value is string
				this.isString = true;
				this.stringValue = System.Text.Encoding.UTF8.GetString(input, 0, indexOfNull);

				this.bytesValue = null;
			}
			else
			{
				// Otherwise it is byte array
				this.isString = false;
				this.bytesValue = input;

				this.stringValue = null;
			}
		}
	}
}