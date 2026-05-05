using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

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
	/// Only constructor
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
}