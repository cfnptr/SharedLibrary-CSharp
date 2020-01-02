using System.IO;
using System.Net;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account factory class
    /// </summary>
    public class AccountFactory : IAccountFactory<IAccount>
    {
        /// <summary>
        /// Account data byte array size
        /// </summary>
        public int ByteArraySize => Account.ByteSize;

        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create()
        {
            return new Account();
        }
        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create(BinaryReader binaryReader)
        {
            return new Account(binaryReader);
        }
        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create(long id, bool isBlocked, byte level, Username name, Passhash passhash, EmailAddress emailAddress, Token accessToken, IPAddress ipAddress)
        {
            return new Account(id, isBlocked, level, name, passhash, emailAddress, accessToken, ipAddress);
        }
    }
}
