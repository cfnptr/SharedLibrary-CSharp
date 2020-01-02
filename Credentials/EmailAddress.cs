using InjectorGames.SharedLibrary.Collections.Bytes;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace InjectorGames.SharedLibrary.Credentials
{
    /// <summary>
    /// Email adderss container
    /// </summary>
    public class EmailAddress : IByteArray, IComparable, IComparable<EmailAddress>, IEquatable<EmailAddress>
    {
        /// <summary>
        /// Email address byte array size (1 size + 255 address)
        /// </summary>
        public const int ByteSize = 256;
        /// <summary>
        /// Minimum length of the email address (custom limitation)
        /// </summary>
        public const int MinLength = 5;
        /// <summary>
        /// Maximum length of the email address (custom limitation)
        /// </summary>
        public const int MaxLength = 255;

        /// <summary>
        /// Email address byte array size (1 size + 255 address)
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Email address value
        /// </summary>
        protected readonly string value;

        /// <summary>
        /// Creates a new email address container class instance
        /// </summary>
        public EmailAddress(string address)
        {
            if (!IsValidLength(address.Length))
                throw new ArgumentException("Invalid email address length");

            value = address;

            var mailAddress = new MailAddress(address);
            if (address != mailAddress.Address)
                throw new ArgumentException("Invalid email address");
        }
        /// <summary>
        /// Creates a new email address container class instance
        /// </summary>
        public EmailAddress(BinaryReader binaryReader)
        {
            var addressLength = binaryReader.ReadByte();
            var bytes = binaryReader.ReadBytes(addressLength);
            binaryReader.BaseStream.Seek(ByteSize - addressLength, SeekOrigin.Current);

            value = Encoding.ASCII.GetString(bytes);

            var mailAddress = new MailAddress(value);
            if (value != mailAddress.Address)
                throw new ArgumentException("Invalid email address");
        }

        /// <summary>
        /// Returns true if the email address is equal to the object
        /// </summary>
        public override bool Equals(object obj)
        {
            return value.Equals(((EmailAddress)obj).value);
        }
        /// <summary>
        /// Returns email address hash code 
        /// </summary>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        /// <summary>
        /// Returns email address string value
        /// </summary>
        public override string ToString()
        {
            return value;
        }

        /// <summary>
        /// Compares email address to the object
        /// </summary>
        public int CompareTo(object obj)
        {
            return value.CompareTo(((EmailAddress)obj).value);
        }
        /// <summary>
        /// Compares two email addresses
        /// </summary>
        public int CompareTo(EmailAddress other)
        {
            return value.CompareTo(other.value);
        }
        /// <summary>
        /// Returns true if two email addresses is equal
        /// </summary>
        public bool Equals(EmailAddress other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Converts email address value to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            var length = (byte)value.Length;
            binaryWriter.Write(length);

            var bytes = Encoding.ASCII.GetBytes(value);
            binaryWriter.Write(bytes);
            binaryWriter.Seek(ByteSize - length, SeekOrigin.Current);
        }

        /// <summary>
        /// Returns true if the email address has valid length
        /// </summary>
        public static bool IsValidLength(int length)
        {
            return length >= MinLength && length <= MaxLength;
        }

        public static bool operator ==(EmailAddress a, EmailAddress b) { return a.value == b.value; }
        public static bool operator !=(EmailAddress a, EmailAddress b) { return a.value != b.value; }
        public static bool operator ==(EmailAddress a, string b) { return a.value == b; }
        public static bool operator !=(EmailAddress a, string b) { return a.value != b; }
        public static bool operator ==(string a, EmailAddress b) { return a == b.value; }
        public static bool operator !=(string a, EmailAddress b) { return a != b.value; }

        public static implicit operator string(EmailAddress v) { return v.value; }
        public static implicit operator EmailAddress(string v) { return new EmailAddress(v); }
    }
}
