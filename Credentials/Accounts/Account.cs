using InjectorGames.SharedLibrary.Extensions;
using System.IO;
using System.Net;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account container class
    /// </summary>
    public class Account : IAccount
    {
        /// <summary>
        /// Account data container byte array size
        /// </summary>
        public const int ByteSize = sizeof(long) + sizeof(byte) * 2 + Username.ByteSize + Passhash.ByteSize + EmailAddress.ByteSize + Token.ByteSize + IPAddressExtension.ByteSize;

        /// <summary>
        /// Account data container byte array size
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Account identifier
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Is account has blocked
        /// </summary>
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Account level (status)
        /// </summary>
        public byte Level { get; set; }
        /// <summary>
        /// Account name
        /// </summary>
        public Username Name { get; set; }
        /// <summary>
        /// Account passhash
        /// </summary>
        public Passhash Passhash { get; set; }
        /// <summary>
        /// Account email address
        /// </summary>
        public EmailAddress EmailAddress { get; set; }
        /// <summary>
        /// Last account access token
        /// </summary>
        public Token AccessToken { get; set; }
        /// <summary>
        /// Last account use ip address
        /// </summary>
        public IPAddress LastUseIpAddress { get; set; }

        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account() { }
        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account(long id, bool isBlocked, byte level, Username name, Passhash passhash, EmailAddress emailAddress, Token accessToken, IPAddress lastUseIpAddress)
        {
            ID = id;
            IsBlocked = isBlocked;
            Level = level;
            Name = name;
            Passhash = passhash;
            EmailAddress = emailAddress;
            AccessToken = accessToken;
            LastUseIpAddress = lastUseIpAddress;
        }
        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account(BinaryReader binaryReader)
        {
            ID = binaryReader.ReadInt64();
            IsBlocked = binaryReader.ReadBoolean();
            Level = binaryReader.ReadByte();
            Name = new Username(binaryReader);
            Passhash = new Passhash(binaryReader);
            EmailAddress = new EmailAddress(binaryReader);
            AccessToken = new Token(binaryReader);
            LastUseIpAddress = IPAddressExtension.FromBytes(binaryReader);
        }

        /// <summary>
        /// Returns true if the account is equal to the object
        /// </summary>
        public override bool Equals(object obj)
        {
            return ID.Equals(((IAccount)obj).ID);
        }
        /// <summary>
        /// Returns account hash code 
        /// </summary>
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
        /// <summary>
        /// Returns account string value
        /// </summary>
        public override string ToString()
        {
            return ID.ToString();
        }

        /// <summary>
        /// Compares account to the object
        /// </summary>
        public int CompareTo(object obj)
        {
            return ID.CompareTo(((IAccount)obj).ID);
        }
        /// <summary>
        /// Compares two accounts
        /// </summary>
        public int CompareTo(IAccount other)
        {
            return ID.CompareTo(other.ID);
        }
        /// <summary>
        /// Returns true if two accounts is equal
        /// </summary>
        public bool Equals(IAccount other)
        {
            return ID.Equals(other.ID);
        }

        /// <summary>
        /// Converts account data container to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(ID);
            binaryWriter.Write(IsBlocked);
            binaryWriter.Write(Level);
            Name.ToBytes(binaryWriter);
            Passhash.ToBytes(binaryWriter);
            EmailAddress.ToBytes(binaryWriter);
            AccessToken.ToBytes(binaryWriter);
            LastUseIpAddress.ToBytes(binaryWriter);
        }

        /// <summary>
        /// Sets blocked flag and overrides acces token
        /// </summary>
        public void BlockAccount()
        {
            IsBlocked = true;
            AccessToken = Token.Create();
        }
    }
}
