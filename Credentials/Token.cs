using InjectorGames.SharedLibrary.Collections.Bytes;
using System;
using System.IO;
using System.Numerics;

namespace InjectorGames.SharedLibrary.Credentials
{
    /// <summary>
    /// Secure token container class
    /// </summary>
    public class Token : IByteArray, IComparable, IComparable<Token>, IEquatable<Token>
    {
        /// <summary>
        /// Token value size in bytes
        /// </summary>
        public const int ByteSize = 32;

        /// <summary>
        /// Token value size in bytes
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Token big integer value
        /// </summary>
        protected readonly BigInteger value;

        /// <summary>
        /// Creates a new token container class instance
        /// </summary>
        public Token(BigInteger value)
        {
            var bytes = value.ToByteArray();

            if (!IsValidLength(bytes.Length))
                throw new ArgumentException("Invalid token length");

            this.value = value;
        }
        /// <summary>
        /// Creates a new token container class instance
        /// </summary>
        public Token(byte[] bytes)
        {
            if (!IsValidLength(bytes.Length))
                throw new ArgumentException("Invalid token length");

            value = new BigInteger(bytes);
        }
        /// <summary>
        /// Creates a new token container class instance
        /// </summary>
        public Token(BinaryReader binaryReader)
        {
            var bytes = binaryReader.ReadBytes(ByteSize);
            value = new BigInteger(bytes);
        }
        /// <summary>
        /// Creates a new token container class instance
        /// </summary>
        public Token(string base64)
        {
            var bytes = Convert.FromBase64String(base64);

            if (!IsValidLength(bytes.Length))
                throw new ArgumentException("Invalid token length");

            value = new BigInteger(bytes);
        }

        /// <summary>
        /// Returns true if the username is equal to the object
        /// </summary>
        public override bool Equals(object obj)
        {
            return value.Equals(((Token)obj).value);
        }
        /// <summary>
        /// Returns username hash code 
        /// </summary>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        /// <summary>
        /// Returns username string value
        /// </summary>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        /// Compares token to the object
        /// </summary>
        public int CompareTo(object obj)
        {
            return value.CompareTo(((Token)obj).value);
        }
        /// <summary>
        /// Compares two tokens
        /// </summary>
        public int CompareTo(Token other)
        {
            return value.CompareTo(other.value);
        }
        /// <summary>
        /// Returns true if tokens is equal
        /// </summary>
        public bool Equals(Token other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Converts token value to the base 64 string value
        /// </summary>
        public string ToBase64()
        {
            var bytes = value.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts token value to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(value.ToByteArray());
        }

        /// <summary>
        /// Returns true if the token has valid length
        /// </summary>
        public static bool IsValidLength(int length)
        {
            return length == ByteSize;
        }

        /// <summary>
        /// Creates a new cryptographically secure token instance
        /// </summary>
        public static Token Create()
        {
            return new Token(Cryptographer.GetSecureRandom(ByteSize));
        }

        public static bool operator ==(Token a, Token b) { return a.value == b.value; }
        public static bool operator !=(Token a, Token b) { return a.value != b.value; }
        public static bool operator ==(Token a, BigInteger b) { return a.value == b; }
        public static bool operator !=(Token a, BigInteger b) { return a.value != b; }
        public static bool operator ==(BigInteger a, Token b) { return a == b.value; }
        public static bool operator !=(BigInteger a, Token b) { return a != b.value; }

        public static implicit operator BigInteger(Token v) { return v.value; }
        public static implicit operator Token(BigInteger v) { return new Token(v); }
        public static implicit operator byte[](Token v) { return v.value.ToByteArray(); }
        public static implicit operator Token(byte[] v) { return new Token(v); }
    }
}
