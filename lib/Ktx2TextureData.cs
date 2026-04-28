using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Buffers.Binary;

namespace KtxSharp;

/// <summary>
/// KTX2 texture data class
/// </summary>
public sealed class Ktx2TextureData
{
	// Mip Level Array

	/// <summary>
	/// List of byte arrays holding all the image data for every level
	/// </summary>
	public readonly List<byte[]> levelImages = new List<byte[]>();

	/// <summary>
	/// Only constructor
	/// </summary>
	/// <param name="stream">Stream for reading</param>
	/// <param name="levelIndexes">List of level indexes</param>
	public Ktx2TextureData(Stream stream, List<LevelIndex> levelIndexes)
	{
		// Use the stream in a binary reader.
		using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, leaveOpen: true))
		{
			// Mip Level Array

			for (int i = 0; i < levelIndexes.Count; i++)
			{
				levelImages.Add(reader.ReadBytes((int)levelIndexes[i].byteLength));
			}
		}
	}
}