// Class for storing texture data
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace KtxSharp
{
	public sealed class KtxTextureData
	{
		public readonly uint totalTextureDataLength;

		public readonly byte[] textureDataAsRawBytes;

		public readonly TextureTypeBasic textureType;

		public List<byte[]> textureDataOfMipmapLevel;

		public KtxTextureData(KtxHeader header, MemoryStream memoryStream)
		{
			this.totalTextureDataLength = (uint)memoryStream.Length;

			// Try to figure out texture type basic
			bool containsMipmaps = header.numberOfMipmapLevels > 1;

			if (header.numberOfArrayElements == 0 || header.numberOfArrayElements == 1)
			{
				// Is NOT texture array

				if (header.numberOfFaces == 0 || header.numberOfFaces == 1)
				{
					// Is NOT cube texture

					if (header.pixelDepth == 0 || header.pixelDepth == 1)
					{
						// Is not 3D texture

						if (header.pixelHeight == 0 || header.pixelHeight == 1)
						{
							// 1D texture
							this.textureType = containsMipmaps ? TextureTypeBasic.Basic1DWithMipmaps : TextureTypeBasic.Basic1DNoMipmaps;
						}
						else
						{
							// 2D texture
							this.textureType = containsMipmaps ? TextureTypeBasic.Basic2DWithMipmaps : TextureTypeBasic.Basic2DNoMipmaps;
						}
					}
					else
					{
						// Is 3D texture
						this.textureType = containsMipmaps ? TextureTypeBasic.Basic3DWithMipmaps : TextureTypeBasic.Basic3DNoMipmaps;
					}
				}
				else
				{
					// Is cube texture
				}
			}
			else
			{
				// Is Texture array
			}

			uint mipmapLevels = header.numberOfMipmapLevels;
			if (mipmapLevels == 0)
			{
				mipmapLevels = 1;
			}

			// Since we know how many mipmap levels there are, allocate the capacity
			this.textureDataOfMipmapLevel = new List<byte[]>((int)mipmapLevels);

			// Check if length reads should be endian swapped
			bool shouldSwapEndianness = (header.endiannessValue != Common.expectedEndianValue);

			using (BinaryReader reader = new BinaryReader(memoryStream, Encoding.UTF8, leaveOpen: true))
			{
				for (int i = 0; i < mipmapLevels; i++)
				{
					uint amountOfDataInThisMipmapLevel = shouldSwapEndianness ? KtxBitFiddling.SwapEndian(reader.ReadUInt32()) : reader.ReadUInt32();
					this.textureDataOfMipmapLevel.Add(reader.ReadBytes((int)amountOfDataInThisMipmapLevel));

					// Skip possible padding bytes
					while (amountOfDataInThisMipmapLevel % 4 != 0)
					{
						amountOfDataInThisMipmapLevel++;
						// Read but ignore values
						reader.ReadByte();
					}
				}
			}			
		}
	}
}