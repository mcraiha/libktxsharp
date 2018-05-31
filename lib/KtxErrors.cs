
namespace KtxSharp
{
	public static class ErrorGen
	{
		public static string Modulo4Error(string variableName, uint value)
		{
			return $"{variableName} value is {value}, but it should be modulo 4!";
		}
	}
}