using NUnit.Framework;
using KtxSharp;

namespace Tests
{
    public class MetadataValueTests
    {
        [Test]
        public void ConstructorTest()
        {
            // Arrange
			byte[] val1 = new byte[] { 0x61 };
			byte[] val2 = new byte[] { 0x61, 0 };
			byte[] val3 = new byte[] { 0x6A, 0x6F, 0x6B, 0x65, 0x32, 0x00 }; // UTF8 v: 'joke2\0'


			// Act
			MetadataValue result1 = new MetadataValue(val1);
			MetadataValue result2 = new MetadataValue(val2);
			MetadataValue result3 = new MetadataValue(val3);

			// Assert
			CollectionAssert.AreNotEqual(val1, val2);
			CollectionAssert.AreNotEqual(val1, val3);

			Assert.IsFalse(result1.isString);
			Assert.AreEqual(val1[0], result1.bytesValue[0]);

			Assert.IsTrue(result2.isString);
			Assert.AreEqual("a", result2.stringValue);

			Assert.IsTrue(result3.isString);
			Assert.AreEqual("joke2", result3.stringValue);
        }
    }
}