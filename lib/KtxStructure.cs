

namespace KtxSharp
{
	public sealed class KtxStructure
	{
		public readonly KtxHeader header;
		
		public readonly KtxTextureData textureData;

		public KtxStructure(KtxHeader ktxHeader, KtxTextureData texData)
		{
			this.header = ktxHeader;
			this.textureData = texData;
		}
	}
}