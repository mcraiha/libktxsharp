
namespace KtxSharp;

/// <summary>
/// Ktx2Structure that has header, supercompression data and texture data
/// </summary>
public sealed class Ktx2Structure
{
	/// <summary>
	/// KTX2 header
	/// </summary>
	public readonly Ktx2Header ktx2Header;

	/// <summary>
	/// KTX2 supercompression data
	/// </summary>
	public readonly Ktx2Supercompression ktx2Supercompression;
	
	/// <summary>
	/// KTX2 texture data
	/// </summary>
	public readonly Ktx2TextureData textureData;

	/// <summary>
	/// Constuctor for Ktx2Structure
	/// </summary>
	/// <param name="header">KTX2 header</param>
	/// <param name="supercompression">KTX2 supercompression data</param>
	/// <param name="texData">KTX2 texture data</param>
	public Ktx2Structure(Ktx2Header header, Ktx2Supercompression supercompression, Ktx2TextureData texData)
	{
		this.ktx2Header = header;
		this.ktx2Supercompression = supercompression;
		this.textureData = texData;
	}
}
