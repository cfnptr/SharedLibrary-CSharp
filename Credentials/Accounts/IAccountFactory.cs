using InjectorGames.SharedLibrary.Collections.Bytes;
using System.Net;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account factory interface
    /// </summary>
    public interface IAccountFactory<T> : IByteArrayFactory<T> where T : IAccount
    {
        /// <summary>
        /// Creates a new account instance
        /// </summary>
        T Create(long id, bool isBlocked, byte level, Username name, Passhash passhash, EmailAddress emailAddress, Token accessToken, IPAddress ipAddress);
    }
}
