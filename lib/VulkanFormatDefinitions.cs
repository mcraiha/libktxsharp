namespace KtxSharp;

/// <summary>
/// Format definitions
/// </summary>
/// <remarks>Source: https://docs.vulkan.org/spec/latest/chapters/formats.html</remarks>
public enum VkFormat : uint
{
	/// <summary>
	/// The format is not specified
	/// </summary>
	VK_FORMAT_UNDEFINED = 0,

	/// <summary>
	/// Two-component, 8-bit packed unsigned normalized format that has a 4-bit R component in bits 4..7, and a 4-bit G component in bits 0..3
	/// </summary>
	VK_FORMAT_R4G4_UNORM_PACK8 = 1,

	/// <summary>
	/// Four-component, 16-bit packed unsigned normalized format that has a 4-bit R component in bits 12..15, a 4-bit G component in bits 8..11, a 4-bit B component in bits 4..7, and a 4-bit A component in bits 0..3
	/// </summary>
	VK_FORMAT_R4G4B4A4_UNORM_PACK16 = 2,

	/// <summary>
	/// Four-component, 16-bit packed unsigned normalized format that has a 4-bit B component in bits 12..15, a 4-bit G component in bits 8..11, a 4-bit R component in bits 4..7, and a 4-bit A component in bits 0..3
	/// </summary>
	VK_FORMAT_B4G4R4A4_UNORM_PACK16 = 3,

	/// <summary>
	/// Three-component, 16-bit packed unsigned normalized format that has a 5-bit R component in bits 11..15, a 6-bit G component in bits 5..10, and a 5-bit B component in bits 0..4
	/// </summary>
	VK_FORMAT_R5G6B5_UNORM_PACK16 = 4,

	/// <summary>
	/// Three-component, 16-bit packed unsigned normalized format that has a 5-bit B component in bits 11..15, a 6-bit G component in bits 5..10, and a 5-bit R component in bits 0..4
	/// </summary>
	VK_FORMAT_B5G6R5_UNORM_PACK16 = 5,

	/// <summary>
	/// Four-component, 16-bit packed unsigned normalized format that has a 5-bit R component in bits 11..15, a 5-bit G component in bits 6..10, a 5-bit B component in bits 1..5, and a 1-bit A component in bit 0
	/// </summary>
	VK_FORMAT_R5G5B5A1_UNORM_PACK16 = 6,

	/// <summary>
	/// Four-component, 16-bit packed unsigned normalized format that has a 5-bit B component in bits 11..15, a 5-bit G component in bits 6..10, a 5-bit R component in bits 1..5, and a 1-bit A component in bit 0
	/// </summary>
	VK_FORMAT_B5G5R5A1_UNORM_PACK16 = 7,

	/// <summary>
	/// Four-component, 16-bit packed unsigned normalized format that has a 1-bit A component in bit 15, a 5-bit R component in bits 10..14, a 5-bit G component in bits 5..9, and a 5-bit B component in bits 0..4
	/// </summary>
	VK_FORMAT_A1R5G5B5_UNORM_PACK16 = 8,

	/// <summary>
	/// One-component, 8-bit unsigned normalized format that has a single 8-bit R component
	/// </summary>
	VK_FORMAT_R8_UNORM = 9,

	/// <summary>
	/// One-component, 8-bit signed normalized format that has a single 8-bit R component
	/// </summary>
	VK_FORMAT_R8_SNORM = 10,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8_USCALED = 11,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8_SSCALED = 12,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8_UINT = 13,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8_SINT = 14,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8_SRGB = 15,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_UNORM = 16,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_SNORM = 17,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_USCALED = 18,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_SSCALED = 19,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_UINT = 20,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_SINT = 21,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8_SRGB = 22,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_UNORM = 23,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_SNORM = 24,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_USCALED = 25,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_SSCALED = 26,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_UINT = 27,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_SINT = 28,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8_SRGB = 29,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_UNORM = 30,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_SNORM = 31,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_USCALED = 32,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_SSCALED = 33,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_UINT = 34,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_SINT = 35,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8_SRGB = 36,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_UNORM = 37,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_SNORM = 38,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_USCALED = 39,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_SSCALED = 40,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_UINT = 41,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_SINT = 42,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R8G8B8A8_SRGB = 43,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_UNORM = 44,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_SNORM = 45,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_USCALED = 46,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_SSCALED = 47,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_UINT = 48,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_SINT = 49,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B8G8R8A8_SRGB = 50,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_UNORM_PACK32 = 51,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_SNORM_PACK32 = 52,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_USCALED_PACK32 = 53,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_SSCALED_PACK32 = 54,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_UINT_PACK32 = 55,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_SINT_PACK32 = 56,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A8B8G8R8_SRGB_PACK32 = 57,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_UNORM_PACK32 = 58,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_SNORM_PACK32 = 59,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_USCALED_PACK32 = 60,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_SSCALED_PACK32 = 61,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_UINT_PACK32 = 62,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2R10G10B10_SINT_PACK32 = 63,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_UNORM_PACK32 = 64,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_SNORM_PACK32 = 65,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_USCALED_PACK32 = 66,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_SSCALED_PACK32 = 67,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_UINT_PACK32 = 68,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_A2B10G10R10_SINT_PACK32 = 69,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_UNORM = 70,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_SNORM = 71,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_USCALED = 72,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_SSCALED = 73,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_UINT = 74,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_SINT = 75,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16_SFLOAT = 76,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_UNORM = 77,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_SNORM = 78,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_USCALED = 79,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_SSCALED = 80,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_UINT = 81,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_SINT = 82,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16_SFLOAT = 83,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_UNORM = 84,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_SNORM = 85,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_USCALED = 86,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_SSCALED = 87,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_UINT = 88,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_SINT = 89,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16_SFLOAT = 90,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_UNORM = 91,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_SNORM = 92,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_USCALED = 93,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_SSCALED = 94,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_UINT = 95,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_SINT = 96,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R16G16B16A16_SFLOAT = 97,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32_UINT = 98,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32_SINT = 99,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32_SFLOAT = 100,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32_UINT = 101,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32_SINT = 102,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32_SFLOAT = 103,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32_UINT = 104,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32_SINT = 105,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32_SFLOAT = 106,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32A32_UINT = 107,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32A32_SINT = 108,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R32G32B32A32_SFLOAT = 109,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64_UINT = 110,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64_SINT = 111,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64_SFLOAT = 112,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64_UINT = 113,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64_SINT = 114,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64_SFLOAT = 115,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64_UINT = 116,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64_SINT = 117,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64_SFLOAT = 118,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64A64_UINT = 119,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64A64_SINT = 120,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_R64G64B64A64_SFLOAT = 121,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_B10G11R11_UFLOAT_PACK32 = 122,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_E5B9G9R9_UFLOAT_PACK32 = 123,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_D16_UNORM = 124,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_X8_D24_UNORM_PACK32 = 125,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_D32_SFLOAT = 126,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_S8_UINT = 127,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_D16_UNORM_S8_UINT = 128,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_D24_UNORM_S8_UINT = 129,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_D32_SFLOAT_S8_UINT = 130,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC1_RGB_UNORM_BLOCK = 131,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC1_RGB_SRGB_BLOCK = 132,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC1_RGBA_UNORM_BLOCK = 133,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC1_RGBA_SRGB_BLOCK = 134,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC2_UNORM_BLOCK = 135,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC2_SRGB_BLOCK = 136,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC3_UNORM_BLOCK = 137,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC3_SRGB_BLOCK = 138,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC4_UNORM_BLOCK = 139,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC4_SNORM_BLOCK = 140,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC5_UNORM_BLOCK = 141,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC5_SNORM_BLOCK = 142,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC6H_UFLOAT_BLOCK = 143,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC6H_SFLOAT_BLOCK = 144,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC7_UNORM_BLOCK = 145,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_BC7_SRGB_BLOCK = 146,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8_UNORM_BLOCK = 147,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8_SRGB_BLOCK = 148,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8A1_UNORM_BLOCK = 149,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8A1_SRGB_BLOCK = 150,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8A8_UNORM_BLOCK = 151,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ETC2_R8G8B8A8_SRGB_BLOCK = 152,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_EAC_R11_UNORM_BLOCK = 153,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_EAC_R11_SNORM_BLOCK = 154,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_EAC_R11G11_UNORM_BLOCK = 155,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_EAC_R11G11_SNORM_BLOCK = 156,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_4x4_UNORM_BLOCK = 157,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_4x4_SRGB_BLOCK = 158,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_5x4_UNORM_BLOCK = 159,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_5x4_SRGB_BLOCK = 160,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_5x5_UNORM_BLOCK = 161,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_5x5_SRGB_BLOCK = 162,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_6x5_UNORM_BLOCK = 163,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_6x5_SRGB_BLOCK = 164,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_6x6_UNORM_BLOCK = 165,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_6x6_SRGB_BLOCK = 166,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x5_UNORM_BLOCK = 167,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x5_SRGB_BLOCK = 168,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x6_UNORM_BLOCK = 169,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x6_SRGB_BLOCK = 170,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x8_UNORM_BLOCK = 171,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_8x8_SRGB_BLOCK = 172,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x5_UNORM_BLOCK = 173,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x5_SRGB_BLOCK = 174,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x6_UNORM_BLOCK = 175,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x6_SRGB_BLOCK = 176,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x8_UNORM_BLOCK = 177,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x8_SRGB_BLOCK = 178,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x10_UNORM_BLOCK = 179,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_10x10_SRGB_BLOCK = 180,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_12x10_UNORM_BLOCK = 181,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_12x10_SRGB_BLOCK = 182,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_12x12_UNORM_BLOCK = 183,

	/// <summary>
	/// 
	/// </summary>
	VK_FORMAT_ASTC_12x12_SRGB_BLOCK = 184,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8B8G8R8_422_UNORM = 1000156000,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_B8G8R8G8_422_UNORM = 1000156001,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_420_UNORM = 1000156002,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_420_UNORM = 1000156003,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_422_UNORM = 1000156004,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_422_UNORM = 1000156005,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_444_UNORM = 1000156006,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R10X6_UNORM_PACK16 = 1000156007,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R10X6G10X6_UNORM_2PACK16 = 1000156008,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R10X6G10X6B10X6A10X6_UNORM_4PACK16 = 1000156009,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6B10X6G10X6R10X6_422_UNORM_4PACK16 = 1000156010,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_B10X6G10X6R10X6G10X6_422_UNORM_4PACK16 = 1000156011,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_420_UNORM_3PACK16 = 1000156012,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_420_UNORM_3PACK16 = 1000156013,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_422_UNORM_3PACK16 = 1000156014,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_422_UNORM_3PACK16 = 1000156015,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_444_UNORM_3PACK16 = 1000156016,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R12X4_UNORM_PACK16 = 1000156017,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R12X4G12X4_UNORM_2PACK16 = 1000156018,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_R12X4G12X4B12X4A12X4_UNORM_4PACK16 = 1000156019,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4B12X4G12X4R12X4_422_UNORM_4PACK16 = 1000156020,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_B12X4G12X4R12X4G12X4_422_UNORM_4PACK16 = 1000156021,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_420_UNORM_3PACK16 = 1000156022,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_420_UNORM_3PACK16 = 1000156023,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_422_UNORM_3PACK16 = 1000156024,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_422_UNORM_3PACK16 = 1000156025,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_444_UNORM_3PACK16 = 1000156026,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16B16G16R16_422_UNORM = 1000156027,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_B16G16R16G16_422_UNORM = 1000156028,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_420_UNORM = 1000156029,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_420_UNORM = 1000156030,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_422_UNORM = 1000156031,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_422_UNORM = 1000156032,

	/// <remarks>Provided by VK_VERSION_1_1</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_444_UNORM = 1000156033,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_444_UNORM = 1000330000,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_444_UNORM_3PACK16 = 1000330001,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_444_UNORM_3PACK16 = 1000330002,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_444_UNORM = 1000330003,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_A4R4G4B4_UNORM_PACK16 = 1000340000,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_A4B4G4R4_UNORM_PACK16 = 1000340001,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_4x4_SFLOAT_BLOCK = 1000066000,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_5x4_SFLOAT_BLOCK = 1000066001,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_5x5_SFLOAT_BLOCK = 1000066002,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_6x5_SFLOAT_BLOCK = 1000066003,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_6x6_SFLOAT_BLOCK = 1000066004,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_8x5_SFLOAT_BLOCK = 1000066005,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_8x6_SFLOAT_BLOCK = 1000066006,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_8x8_SFLOAT_BLOCK = 1000066007,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_10x5_SFLOAT_BLOCK = 1000066008,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_10x6_SFLOAT_BLOCK = 1000066009,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_10x8_SFLOAT_BLOCK = 1000066010,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_10x10_SFLOAT_BLOCK = 1000066011,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_12x10_SFLOAT_BLOCK = 1000066012,

	/// <remarks>Provided by VK_VERSION_1_3</remarks>
	VK_FORMAT_ASTC_12x12_SFLOAT_BLOCK = 1000066013,

	/// <remarks>Provided by VK_VERSION_1_4</remarks>
	VK_FORMAT_A1B5G5R5_UNORM_PACK16 = 1000470000,

	/// <remarks>Provided by VK_VERSION_1_4</remarks>
	VK_FORMAT_A8_UNORM = 1000470001,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC1_2BPP_UNORM_BLOCK_IMG = 1000054000,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC1_4BPP_UNORM_BLOCK_IMG = 1000054001,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC2_2BPP_UNORM_BLOCK_IMG = 1000054002,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC2_4BPP_UNORM_BLOCK_IMG = 1000054003,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC1_2BPP_SRGB_BLOCK_IMG = 1000054004,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC1_4BPP_SRGB_BLOCK_IMG = 1000054005,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC2_2BPP_SRGB_BLOCK_IMG = 1000054006,

	/// <remarks>Provided by VK_IMG_format_pvrtc</remarks>
	VK_FORMAT_PVRTC2_4BPP_SRGB_BLOCK_IMG = 1000054007,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_3x3x3_UNORM_BLOCK_EXT = 1000288000,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_3x3x3_SRGB_BLOCK_EXT = 1000288001,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_3x3x3_SFLOAT_BLOCK_EXT = 1000288002,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x3x3_UNORM_BLOCK_EXT = 1000288003,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x3x3_SRGB_BLOCK_EXT = 1000288004,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x3x3_SFLOAT_BLOCK_EXT = 1000288005,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x3_UNORM_BLOCK_EXT = 1000288006,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x3_SRGB_BLOCK_EXT = 1000288007,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x3_SFLOAT_BLOCK_EXT = 1000288008,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x4_UNORM_BLOCK_EXT = 1000288009,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x4_SRGB_BLOCK_EXT = 1000288010,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_4x4x4_SFLOAT_BLOCK_EXT = 1000288011,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x4x4_UNORM_BLOCK_EXT = 1000288012,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x4x4_SRGB_BLOCK_EXT = 1000288013,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x4x4_SFLOAT_BLOCK_EXT = 1000288014,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x4_UNORM_BLOCK_EXT = 1000288015,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x4_SRGB_BLOCK_EXT = 1000288016,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x4_SFLOAT_BLOCK_EXT = 1000288017,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x5_UNORM_BLOCK_EXT = 1000288018,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x5_SRGB_BLOCK_EXT = 1000288019,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_5x5x5_SFLOAT_BLOCK_EXT = 1000288020,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x5x5_UNORM_BLOCK_EXT = 1000288021,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x5x5_SRGB_BLOCK_EXT = 1000288022,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x5x5_SFLOAT_BLOCK_EXT = 1000288023,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x5_UNORM_BLOCK_EXT = 1000288024,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x5_SRGB_BLOCK_EXT = 1000288025,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x5_SFLOAT_BLOCK_EXT = 1000288026,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x6_UNORM_BLOCK_EXT = 1000288027,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x6_SRGB_BLOCK_EXT = 1000288028,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_3d</remarks>
	VK_FORMAT_ASTC_6x6x6_SFLOAT_BLOCK_EXT = 1000288029,

	/// <remarks>Provided by VK_ARM_tensors</remarks>
	VK_FORMAT_R8_BOOL_ARM = 1000460000,

	/// <remarks>Provided by VK_KHR_shader_bfloat16 with VK_ARM_tensors</remarks>
	VK_FORMAT_R16_SFLOAT_FPENCODING_BFLOAT16_ARM = 1000460001,

	/// <remarks>Provided by VK_EXT_shader_float8 with VK_ARM_tensors</remarks>
	VK_FORMAT_R8_SFLOAT_FPENCODING_FLOAT8E4M3_ARM = 1000460002,

	/// <remarks>Provided by VK_EXT_shader_float8 with VK_ARM_tensors</remarks>
	VK_FORMAT_R8_SFLOAT_FPENCODING_FLOAT8E5M2_ARM = 1000460003,

	/// <remarks>Provided by VK_NV_optical_flow</remarks>
	VK_FORMAT_R16G16_SFIXED5_NV = 1000464000,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R10X6_UINT_PACK16_ARM = 1000609000,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R10X6G10X6_UINT_2PACK16_ARM = 1000609001,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R10X6G10X6B10X6A10X6_UINT_4PACK16_ARM = 1000609002,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R12X4_UINT_PACK16_ARM = 1000609003,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R12X4G12X4_UINT_2PACK16_ARM = 1000609004,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R12X4G12X4B12X4A12X4_UINT_4PACK16_ARM = 1000609005,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2_UINT_PACK16_ARM = 1000609006,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2G14X2_UINT_2PACK16_ARM = 1000609007,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2G14X2B14X2A14X2_UINT_4PACK16_ARM = 1000609008,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2_UNORM_PACK16_ARM = 1000609009,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2G14X2_UNORM_2PACK16_ARM = 1000609010,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_R14X2G14X2B14X2A14X2_UNORM_4PACK16_ARM = 1000609011,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_G14X2_B14X2R14X2_2PLANE_420_UNORM_3PACK16_ARM = 1000609012,

	/// <remarks>Provided by VK_ARM_format_pack</remarks>
	VK_FORMAT_G14X2_B14X2R14X2_2PLANE_422_UNORM_3PACK16_ARM = 1000609013,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_4x4_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_4x4_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_5x4_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_5x4_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_5x5_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_5x5_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_6x5_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_6x5_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_6x6_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_6x6_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_8x5_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_8x5_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_8x6_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_8x6_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_8x8_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_8x8_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_10x5_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_10x5_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_10x6_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_10x6_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_10x8_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_10x8_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_10x10_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_10x10_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_12x10_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_12x10_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_EXT_texture_compression_astc_hdr</remarks>
	VK_FORMAT_ASTC_12x12_SFLOAT_BLOCK_EXT = VK_FORMAT_ASTC_12x12_SFLOAT_BLOCK,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8B8G8R8_422_UNORM_KHR = VK_FORMAT_G8B8G8R8_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_B8G8R8G8_422_UNORM_KHR = VK_FORMAT_B8G8R8G8_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_420_UNORM_KHR = VK_FORMAT_G8_B8_R8_3PLANE_420_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_420_UNORM_KHR = VK_FORMAT_G8_B8R8_2PLANE_420_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_422_UNORM_KHR = VK_FORMAT_G8_B8_R8_3PLANE_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_422_UNORM_KHR = VK_FORMAT_G8_B8R8_2PLANE_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G8_B8_R8_3PLANE_444_UNORM_KHR = VK_FORMAT_G8_B8_R8_3PLANE_444_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R10X6_UNORM_PACK16_KHR = VK_FORMAT_R10X6_UNORM_PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R10X6G10X6_UNORM_2PACK16_KHR = VK_FORMAT_R10X6G10X6_UNORM_2PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R10X6G10X6B10X6A10X6_UNORM_4PACK16_KHR = VK_FORMAT_R10X6G10X6B10X6A10X6_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6B10X6G10X6R10X6_422_UNORM_4PACK16_KHR = VK_FORMAT_G10X6B10X6G10X6R10X6_422_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_B10X6G10X6R10X6G10X6_422_UNORM_4PACK16_KHR = VK_FORMAT_B10X6G10X6R10X6G10X6_422_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_420_UNORM_3PACK16_KHR = VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_420_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_420_UNORM_3PACK16_KHR = VK_FORMAT_G10X6_B10X6R10X6_2PLANE_420_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_422_UNORM_3PACK16_KHR = VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_422_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_422_UNORM_3PACK16_KHR = VK_FORMAT_G10X6_B10X6R10X6_2PLANE_422_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_444_UNORM_3PACK16_KHR = VK_FORMAT_G10X6_B10X6_R10X6_3PLANE_444_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R12X4_UNORM_PACK16_KHR = VK_FORMAT_R12X4_UNORM_PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R12X4G12X4_UNORM_2PACK16_KHR = VK_FORMAT_R12X4G12X4_UNORM_2PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_R12X4G12X4B12X4A12X4_UNORM_4PACK16_KHR = VK_FORMAT_R12X4G12X4B12X4A12X4_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4B12X4G12X4R12X4_422_UNORM_4PACK16_KHR = VK_FORMAT_G12X4B12X4G12X4R12X4_422_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_B12X4G12X4R12X4G12X4_422_UNORM_4PACK16_KHR = VK_FORMAT_B12X4G12X4R12X4G12X4_422_UNORM_4PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_420_UNORM_3PACK16_KHR = VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_420_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_420_UNORM_3PACK16_KHR = VK_FORMAT_G12X4_B12X4R12X4_2PLANE_420_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_422_UNORM_3PACK16_KHR = VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_422_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_422_UNORM_3PACK16_KHR = VK_FORMAT_G12X4_B12X4R12X4_2PLANE_422_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_444_UNORM_3PACK16_KHR = VK_FORMAT_G12X4_B12X4_R12X4_3PLANE_444_UNORM_3PACK16,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16B16G16R16_422_UNORM_KHR = VK_FORMAT_G16B16G16R16_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_B16G16R16G16_422_UNORM_KHR = VK_FORMAT_B16G16R16G16_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_420_UNORM_KHR = VK_FORMAT_G16_B16_R16_3PLANE_420_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_420_UNORM_KHR = VK_FORMAT_G16_B16R16_2PLANE_420_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_422_UNORM_KHR = VK_FORMAT_G16_B16_R16_3PLANE_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_422_UNORM_KHR = VK_FORMAT_G16_B16R16_2PLANE_422_UNORM,

	/// <remarks>Provided by VK_KHR_sampler_ycbcr_conversion</remarks>
	VK_FORMAT_G16_B16_R16_3PLANE_444_UNORM_KHR = VK_FORMAT_G16_B16_R16_3PLANE_444_UNORM,

	/// <remarks>Provided by VK_EXT_ycbcr_2plane_444_formats</remarks>
	VK_FORMAT_G8_B8R8_2PLANE_444_UNORM_EXT = VK_FORMAT_G8_B8R8_2PLANE_444_UNORM,

	/// <remarks>Provided by VK_EXT_ycbcr_2plane_444_formats</remarks>
	VK_FORMAT_G10X6_B10X6R10X6_2PLANE_444_UNORM_3PACK16_EXT = VK_FORMAT_G10X6_B10X6R10X6_2PLANE_444_UNORM_3PACK16,

	/// <remarks>Provided by VK_EXT_ycbcr_2plane_444_formats</remarks>
	VK_FORMAT_G12X4_B12X4R12X4_2PLANE_444_UNORM_3PACK16_EXT = VK_FORMAT_G12X4_B12X4R12X4_2PLANE_444_UNORM_3PACK16,

	/// <remarks>Provided by VK_EXT_ycbcr_2plane_444_formats</remarks>
	VK_FORMAT_G16_B16R16_2PLANE_444_UNORM_EXT = VK_FORMAT_G16_B16R16_2PLANE_444_UNORM,

	/// <remarks>Provided by VK_EXT_4444_formats</remarks>
	VK_FORMAT_A4R4G4B4_UNORM_PACK16_EXT = VK_FORMAT_A4R4G4B4_UNORM_PACK16,

	/// <remarks>Provided by VK_EXT_4444_formats</remarks>
	VK_FORMAT_A4B4G4R4_UNORM_PACK16_EXT = VK_FORMAT_A4B4G4R4_UNORM_PACK16,

	/// <remarks>Provided by VK_NV_optical_flow
	/// VK_FORMAT_R16G16_S10_5_NV is a legacy alias</remarks>
	VK_FORMAT_R16G16_S10_5_NV = VK_FORMAT_R16G16_SFIXED5_NV,

	/// <remarks>Provided by VK_KHR_maintenance5</remarks>
	VK_FORMAT_A1B5G5R5_UNORM_PACK16_KHR = VK_FORMAT_A1B5G5R5_UNORM_PACK16,

	/// <remarks>Provided by VK_KHR_maintenance5</remarks>
	VK_FORMAT_A8_UNORM_KHR = VK_FORMAT_A8_UNORM,

	/// <summary>
	/// Cannot determine
	/// </summary>
	ERROR = uint.MaxValue
}

/// <summary>
/// Supercompression Schemes
/// </summary>
/// <remarks>See https://github.khronos.org/KTX-Specification/ktxspec.v2.html#_supercompressionscheme</remarks>
public enum SupercompressionScheme : uint
{
	// Common

	/// <summary>
	/// no supercompression
	/// </summary>
	None = 0,

	/// <summary>
	/// BasisLZ
	/// </summary>
	/// <remarks>See https://github.com/BinomialLLC/basis_universal/wiki</remarks>
	BasisLZ = 1,

	/// <summary>
	/// Zstandard Compression
	/// </summary>
	/// <remarks>See https://datatracker.ietf.org/doc/html/rfc8478</remarks>
	Zstandard = 2,

	/// <summary>
	/// ZLIB Compressed Data Format
	/// </summary>
	/// <remarks>See https://datatracker.ietf.org/doc/html/rfc1950</remarks>
	ZLIB = 3,

	// Vendor Supercompression Schemes

	/// <summary>
	/// Asobo
	/// </summary>
	Asobo = 0x10000,

	/// <summary>
	/// Cannot determine
	/// </summary>
	ERROR = uint.MaxValue
}