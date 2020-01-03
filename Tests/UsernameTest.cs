using InjectorGames.SharedLibrary.Credentials;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace InjectorGames.SharedLibrary.Tests
{
    [TestClass]
    public class UsernameTest
    {
        [TestMethod]
        public void TestToString()
        {
            var value = "test_username123";
            var username = new Username(value);
            Assert.AreEqual(value, username.ToString());
        }

        [TestMethod]
        public void TestHashCode()
        {
            var value = "test_username456";
            var username = new Username(value);
            Assert.AreEqual(value.GetHashCode(), username.GetHashCode());
        }

        [TestMethod]
        public void TestSpaceLetters()
        {
            var isThrowed = false;
            try { _ = new Username("test username123"); }
            catch { isThrowed = true; }
            Assert.IsTrue(isThrowed);
        }

        [TestMethod]
        public void TestBigLetters()
        {
            var isThrowed = false;
            try { _ = new Username("test_Username123"); }
            catch { isThrowed = true; }
            Assert.IsTrue(isThrowed);
        }

        [TestMethod]
        public void TestAlphanumericLetters()
        {
            var isThrowed = false;
            try { _ = new Username("test.username123"); }
            catch { isThrowed = true; }
            Assert.IsTrue(isThrowed);
        }

        [TestMethod]
        public void TestToBytes()
        {
            var username = new Username("test_username1");
            var bytes = new byte[Username.ByteSize];

            using (var binaryWriter = new BinaryWriter(new MemoryStream(bytes)))
                username.ToBytes(binaryWriter);

            // ASCII Symbols: 116 == 't', 95 == '_', 0 == null
            Assert.AreEqual(116, bytes[0]);
            Assert.AreEqual(95, bytes[4]);
            Assert.AreEqual(0, bytes[14]);
        }

        [TestMethod]
        public void TestToBytesFull()
        {
            var username = new Username("test_username123");
            var bytes = new byte[Username.ByteSize];

            using (var binaryWriter = new BinaryWriter(new MemoryStream(bytes)))
                username.ToBytes(binaryWriter);

            // ASCII Symbols: 116 == 't', 95 == '_'
            Assert.AreEqual(116, bytes[0]);
            Assert.AreEqual(95, bytes[4]);
        }

        [TestMethod]
        public void TestFromBytes()
        {
            var value = "test_username2";
            var username = new Username(value);
            var bytes = new byte[Username.ByteSize];

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    username.ToBytes(binaryWriter);

                    binaryWriter.Seek(0, SeekOrigin.Begin);

                    using (var binaryReader = new BinaryReader(memoryStream))
                        username = new Username(binaryReader);
                }
            }

            Assert.AreEqual(value, username.ToString());
        }

        [TestMethod]
        public void TestFromBytesFull()
        {
            var value = "test_username123";
            var username = new Username(value);
            var bytes = new byte[Username.ByteSize];

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    username.ToBytes(binaryWriter);

                    binaryWriter.Seek(0, SeekOrigin.Begin);

                    using (var binaryReader = new BinaryReader(memoryStream))
                        username = new Username(binaryReader);
                }
            }

            Assert.AreEqual(value, username.ToString());
        }
    }
}
