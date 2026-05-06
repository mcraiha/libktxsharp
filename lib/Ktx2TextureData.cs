using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.IO.Compression;

namespace KtxSharp;

/// <summary>
/// KTX2 texture data class
/// </summary>
public sealed class Ktx2TextureData
{
	// Mip Level Array

	/// <summary>
	/// Chosen supercompression scheme
	/// </summary>
	public readonly SupercompressionScheme supercompression;

	/// <summary>
	/// List of byte arrays holding all the image data for every level
	/// </summary>
	/// <remarks>Mip levels in the array are ordered from the level with the smallest size images to the largest size images</remarks>
	public readonly List<byte[]> levelImages = new List<byte[]>();

	/// <summary>
	/// Only public constructor
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <param name="levelIndexes">List of level indexes</param>
	/// <param name="supercompressionScheme">Chosen supercompression scheme (can be None)</param>
	public Ktx2TextureData(Stream stream, List<LevelIndex> levelIndexes, SupercompressionScheme supercompressionScheme)
	{
		this.supercompression = supercompressionScheme;

		// Use the stream in a binary reader.
		using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
		{
			// Mip Level Array

			for (int i = 0; i < levelIndexes.Count; i++)
			{
				this.levelImages.Add(reader.ReadBytes((int)levelIndexes[i].byteLength));
				if (supercompressionScheme == SupercompressionScheme.None && (i + 1) < levelIndexes.Count)
				{
					// Skip possible padding bytes
					int skipAmount = (int)(levelIndexes[i+1].byteOffset - levelIndexes[i].byteOffset - levelIndexes[i].byteLength);
					_ = reader.ReadBytes(skipAmount);
				}
			}
		}
	}

	/// <summary>
	/// Create uncompressed Ktx2TextureData from existing Ktx2TextureData
	/// </summary>
	/// <param name="compressed">Copmressed Ktx2TextureData</param>
	private Ktx2TextureData(Ktx2TextureData compressed)
	{
		this.supercompression = SupercompressionScheme.None;

		for (int i = 0; i < compressed.levelImages.Count; i++)
		{
			if (compressed.supercompression == SupercompressionScheme.ZLIB)
			{
				using (MemoryStream outStream = new MemoryStream())
				using (MemoryStream inStream = new MemoryStream(compressed.levelImages[i]))
				using (var decompressStream = new ZLibStream(inStream, CompressionMode.Decompress))
				{
					decompressStream.CopyTo(outStream);
					this.levelImages.Add(outStream.ToArray());
				}
			}
		}
	}

	/// <summary>
	/// Create uncompressed texture data from compressed texzture
	/// </summary>
	/// <returns>Ktx2TextureData</returns>
	/// <exception cref="InvalidOperationException">If operation is not possible</exception>
	/// <exception cref="NotImplementedException">If it has not yet been implemented</exception>
	public Ktx2TextureData CreateUncompressed()
	{
		return this.supercompression switch
		{
			SupercompressionScheme.None => throw new InvalidOperationException("Cannot uncompress textures that are not compressed!"),
			SupercompressionScheme.ZLIB => new Ktx2TextureData(this),
			_ => throw new NotImplementedException("Not implemented yet!")
		};
	}
}