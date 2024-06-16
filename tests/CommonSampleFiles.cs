
namespace Tests
{
	public static class CommonFiles
	{
		// If you add any more test files here, remember to add them to tests.csproj !

		/// <summary>
		/// First sample file is created with Compressonator https://github.com/GPUOpen-Tools/Compressonator
		/// </summary>
		public static readonly string validSample1Filename = "16x16_colors_Compressonator.ktx";

		/// <summary>
		/// Second sample file is created with PVRTexTool https://community.imgtec.com/developers/powervr/tools/pvrtextool/
		/// </summary>
		public static readonly string validSample2Filename = "16x16_colors_PVRTexTool.ktx";

		/// <summary>
		/// Third sample file is created with ETCPACK https://github.com/Ericsson/ETCPACK
		/// </summary>
		public static readonly string validSample3Filename = "testimage_SIGNED_R11_EAC.ktx";

		/// <summary>
		/// Fourth sample file is filled (missing parts filled with zeroes) from KTX specifications https://www.khronos.org/opengles/sdk/tools/KTX/file_format_spec/#4
		/// </summary>
		public static readonly string validSample4Filename = "ktx_specs.ktx";

		/// <summary>
		/// Fifth sample file is from Khronos GitHub repo https://github.com/KhronosGroup/KTX-Software/tree/master/tests/testimages 
		/// </summary>
		public static readonly string validSample5Filename = "etc2-rgba8.ktx";

		/// <summary>
		/// Sixth sample file is created with Compressonator https://github.com/GPUOpen-Tools/Compressonator
		/// </summary>
		public static readonly string validSample6Filename = "smiling_etc_64x64_Compressonator.ktx";
	}
}