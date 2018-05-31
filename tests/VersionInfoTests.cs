using NUnit.Framework;
using KtxSharp;

namespace Tests
{
    public class VersionInfoTests
    {
        [Test]
        public void GetVersionTest()
        {
            // Arrange
			
			// Act
			string version = VersionInfo.GetVersion();

			// Assert
			Assert.IsNotNull(version);
			Assert.Greater(version.Length, 1);
        }
    }
}