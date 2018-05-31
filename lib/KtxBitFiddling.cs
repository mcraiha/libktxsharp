// Bit fiddling

namespace KtxSharp
{
	public static class KtxBitFiddling
	{
		// https://stackoverflow.com/a/19560621/4886769
		public static uint SwapEndian(uint inputValue)
		{
			// swap adjacent 16-bit blocks
			inputValue = (inputValue >> 16) | (inputValue << 16);
			// swap adjacent 8-bit blocks
			return ((inputValue & 0xFF00FF00) >> 8) | ((inputValue & 0x00FF00FF) << 8);
		}
	}
}