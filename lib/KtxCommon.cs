// Common global values
using System.Linq;

namespace KtxSharp
{
	public static class Common
	{
		// There is only one valid file identifier for  KTX header, it is '«', 'K', 'T', 'X', ' ', '1', '1', '»', '\r', '\n', '\x1A', '\n'
		public static readonly byte[] onlyValidIdentifier = new byte[] { 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A };

		public static readonly uint expectedEndianValue = 0x04030201;
		public static readonly uint otherValidEndianValue = 0x01020304;

		public static readonly byte[] bigEndianAsBytes = new byte[] { 0x04, 0x03, 0x02, 0x01 };

		public static readonly byte[] littleEndianAsBytes = new byte[] { 0x01, 0x02, 0x03, 0x04 };

		// NUL is used to terminate UTF-8 strings
		public static readonly byte nulByte = 0;

		// sizeof(uint)
		public static readonly int sizeOfUint = sizeof(uint);

	}

	public enum GlDataType : uint
	{
		Compressed = 0,
		GL_BYTE = 0x1400,
		GL_UNSIGNED_BYTE = 0x1401,
		GL_SHORT = 0x1402,
		GL_UNSIGNED_SHORT = 0x1403,
		GL_FLOAT = 0x1406,
		GL_FIXED = 0x140C,

		// Custom value for situation where parser cannot identify format
		NotKnown = 0xFFFF,
	}

	public enum GlPixelFormat : uint
	{
		GL_ALPHA = 0x1906,
		GL_RGB = 0x1907,
		GL_RGBA = 0x1908,

		// Custom value for situation where parser cannot identify format
		NotKnown = 0xFFFF,
	}


	public enum GlInternalFormat : uint
	{
		GL_COMPRESSED_RGB_S3TC_DXT1_EXT = 0x83F0,
		GL_COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1,
		GL_COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2,
		GL_COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3,

		GL_ETC1_RGB8_OES = 0x8D64,

		GL_COMPRESSED_R11_EAC = 0x9270,
		GL_COMPRESSED_SIGNED_R11_EAC = 0x9271,
		GL_COMPRESSED_RG11_EAC = 0x9272,
		GL_COMPRESSED_SIGNED_RG11_EAC = 0x9273,

		GL_COMPRESSED_RGB8_ETC2 = 0x9274,
		GL_COMPRESSED_SRGB8_ETC2 = 0x9275,
		GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9276,
		GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9277,
		GL_COMPRESSED_RGBA8_ETC2_EAC = 0x9278,
		GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC = 0x9279,

		GL_COMPRESSED_RGBA_ASTC_4x4_KHR = 0x93B0,
		GL_COMPRESSED_RGBA_ASTC_5x4_KHR = 0x93B1,
		GL_COMPRESSED_RGBA_ASTC_5x5_KHR = 0x93B2,
		GL_COMPRESSED_RGBA_ASTC_6x5_KHR = 0x93B3,
		GL_COMPRESSED_RGBA_ASTC_6x6_KHR = 0x93B4,
		GL_COMPRESSED_RGBA_ASTC_8x5_KHR = 0x93B5,
		GL_COMPRESSED_RGBA_ASTC_8x6_KHR = 0x93B6,
		GL_COMPRESSED_RGBA_ASTC_8x8_KHR = 0x93B7,
		GL_COMPRESSED_RGBA_ASTC_10x5_KHR = 0x93B8,
		GL_COMPRESSED_RGBA_ASTC_10x6_KHR = 0x93B9,
		GL_COMPRESSED_RGBA_ASTC_10x8_KHR = 0x93BA,
		GL_COMPRESSED_RGBA_ASTC_10x10_KHR = 0x93BB,
		GL_COMPRESSED_RGBA_ASTC_12x10_KHR = 0x93BC,
		GL_COMPRESSED_RGBA_ASTC_12x12_KHR = 0x93BD,

		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_4x4_KHR = 0x93D0,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x4_KHR = 0x93D1,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_5x5_KHR = 0x93D2,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x5_KHR = 0x93D3,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR = 0x93D4,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x5_KHR = 0x93D5,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x6_KHR = 0x93D6,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_8x8_KHR = 0x93D7,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x5_KHR = 0x93D8,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x6_KHR = 0x93D9,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x8_KHR = 0x93DA,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_10x10_KHR = 0x93DB,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x10_KHR = 0x93DC,
		GL_COMPRESSED_SRGB8_ALPHA8_ASTC_12x12_KHR = 0x93DD,

		// Custom value for situation where parser cannot identify format
		NotKnown = 0xFFFF,
	}

	public enum TextureTypeBasic
	{
		Basic2DNoMipmaps = 1,
		Basic2DWithMipmaps = 2,
		Basic3DNoMipmaps = 3,
		Basic3DWithMipmaps = 4,
		Basic1DNoMipmaps = 5,
		Basic1DWithMipmaps = 6,
	}
	
}