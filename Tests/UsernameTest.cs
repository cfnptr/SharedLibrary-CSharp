using InjectorGames.SharedLibrary.Credentials;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace InjectorGames.SharedLibrary.Tests
{
    [TestClass]
    public class UsernameTest
    {
        public const string value = "test_username0";

        [TestMethod]
        public void Test()
        {
            var username = new Username(value);

            if (!(value == username) || value != username)
                throw new Exception("Username is not equal to the value");

            var isThrowed = false;
            try { username = new Username("test username0"); }
            catch { isThrowed = true; }
            if (!isThrowed)
                throw new Exception("Available username value with spaces");

            isThrowed = false;
            try { username = new Username("Test_username0"); }
            catch { isThrowed = true; }
            if (!isThrowed)
                throw new Exception("Available username big letterts");

            isThrowed = false;
            try { username = new Username("test.username0"); }
            catch { isThrowed = true; }
            if (!isThrowed)
                throw new Exception("Available username value with not alphanumeric values");

            if (username.ToString() != value)
                throw new Exception("Wrong to string username method");

            var bytes = new byte[Username.ByteSize];

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                    username.ToBytes(binaryWriter);
            }

            // ASCII Symbols: 116 == 't', 95 == '_', 0 == null
            if (bytes[0] != 116 || bytes[4] != 95 || bytes[15] != 0)
                throw new Exception("Wrong username byte array");

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var binaryReader = new BinaryReader(memoryStream))
                    username = new Username(binaryReader);
            }

            if (value != username)
                throw new Exception("Wrong username byte array value");
        }
    }
}
