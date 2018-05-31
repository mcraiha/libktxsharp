using NUnit.Framework;
using KtxSharp;
using System.IO;

namespace Tests
{
	public class KtxValidatorsTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void GenericStreamValidationTest()
		{
			// Arrange
			MemoryStream ms = new MemoryStream(new byte[] { 0 });
			// Close MemoryStream since then it should have CanRead as false https://msdn.microsoft.com/en-us/library/system.io.memorystream.canread.aspx
			ms.Close();

			MemoryStream notMuchContent = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0});

			// Act
			var nullShouldError = KtxValidators.GenericStreamValidation(null);
			var closedMemoryStreamShouldError = KtxValidators.GenericStreamValidation(ms);
			var notEnoughContentShouldError = KtxValidators.GenericStreamValidation(notMuchContent);

			// Assert
			Assert.IsFalse(nullShouldError.isValid);
			Assert.IsTrue(nullShouldError.possibleError.Contains("is null"));

			Assert.IsFalse(closedMemoryStreamShouldError.isValid);
			Assert.IsTrue(closedMemoryStreamShouldError.possibleError.Contains("not readable"));

			Assert.IsFalse(notEnoughContentShouldError.isValid);
			Assert.IsTrue(notEnoughContentShouldError.possibleError.Contains("should have at"));
		}

		[Test]
		public void Test1()
		{
			// Arrange

			// Act

			// Assert
			
		}
	}
}