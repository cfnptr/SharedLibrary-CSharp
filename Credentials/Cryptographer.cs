using System.Security.Cryptography;
using System.Text;

namespace InjectorGames.SharedLibrary.Credentials
{
    /// <summary>
    /// Cryptography usefull method container class
    /// </summary>
    public static class Cryptographer
    {
        /// <summary>
        /// SHA512 hash byte array size
        /// </summary>
        public const int ByteSizeSHA512 = 64; // (512 / 8)

        /// <summary>
        /// Returns byte array SHA512 hash value
        /// </summary>
        public static byte[] ToSHA512(byte[] array)
        {
            using var encryptor = SHA512.Create();
            return encryptor.ComputeHash(array);
        }
        /// <summary>
        /// Returns byte array SHA512 hash value
        /// </summary>
        public static byte[] ToSHA512(byte[] array, int iterationCount)
        {
            using (var encryptor = SHA512.Create())
            {
                for (int i = 0; i < iterationCount; i++)
                    array = encryptor.ComputeHash(array);
            }

            return array;
        }
        /// <summary>
        /// Returns string SHA512 hash value
        /// </summary>
        public static byte[] ToSHA512(string value, Encoding encoding)
        {
            var array = encoding.GetBytes(value);
            return ToSHA512(array);
        }
        /// <summary>
        /// Returns string SHA512 hash value
        /// </summary>
        public static byte[] ToSHA512(string value, Encoding encoding, int iterationCount)
        {
            var array = encoding.GetBytes(value);
            return ToSHA512(array, iterationCount);
        }

        /// <summary>
        /// Fills an array of bytes with a cryptographically strong sequence of random values
        /// </summary>
        public static void FillWithSecureRandom(byte[] array)
        {
            using var rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(array);
        }
        /// <summary>
        /// Returns an array of bytes with a cryptographically strong sequence of random values
        /// </summary>
        public static byte[] GetSecureRandom(int size)
        {
            var array = new byte[size];
            using var rngCsp = new RNGCryptoServiceProvider();
            rngCsp.GetBytes(array);
            return array;
        }
    }
}
