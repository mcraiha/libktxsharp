using NUnit.Framework;
using KtxSharp;
using System.Collections.Generic;

namespace Tests
{
	public class KtxBitFiddlingTests
	{

		[Test]
		public void SwapEndianTest()
		{
			// Arrange
			var inputOutputExpectedPairs = new Dictionary<uint, uint>()
			{
				// First some mirror matches
				{ 0, 0 },
				{ 0xaaaa_aaaa, 0xaaaa_aaaa },
				{ 0xabcd_dcba, 0xbadc_cdab },
				{ uint.MaxValue, uint.MaxValue },

				// Then some other values
				{ 1, 16777216 },
				{ 2048, 524288 }
			};

			// Act
			

			// Assert
			foreach (var pair in inputOutputExpectedPairs)
			{
				Assert.AreEqual(pair.Value, KtxBitFiddling.SwapEndian(pair.Key), $"{pair.Key} with endian swap should be {pair.Value}");
			}
		}
	}
}