using System;
using System.IO;
using System.Text;

namespace KtxSharp;

public sealed class Ktx2Supercompression
{
	// Supercompression Global Data

	/// <summary>
	/// All bytes of supercompression global data
	/// </summary>
	public readonly byte[] supercompressionGlobalDataRaw;

	/// <summary>
	/// Only constructor
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <param name="sgdByteLength">How many bytes will be read for supercompression global data</param>
	public Ktx2Supercompression(Stream stream, ulong sgdByteLength)
	{
		// Use the stream in a binary reader.
		using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
		{
			// Supercompression Global Data

			this.supercompressionGlobalDataRaw = reader.ReadBytes((int)sgdByteLength);
		}
	}
}