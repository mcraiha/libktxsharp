using System.Reflection;

namespace KtxSharp
{
	public static class VersionInfo
	{
		public static string GetVersion()
		{
			var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
			return version;
		}
	}
}