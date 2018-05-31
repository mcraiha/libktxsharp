// MetadataValue class
using System;
using System.Linq;

namespace KtxSharp
{
	public class MetadataValue
	{

		public readonly bool isString;
		public readonly byte[] bytesValue;
		public readonly string stringValue;

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