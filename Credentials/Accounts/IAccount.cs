using InjectorGames.SharedLibrary.Collections.Bytes;
using InjectorGames.SharedLibrary.Entities;
using System;
using System.Net;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account container interface
    /// </summary>
    public interface IAccount : IByteArray, IIdentifiable<long>, INameable<Username>, IComparable, IComparable<IAccount>, IEquatable<IAccount>
    {
        /// <summary>
        /// Is account has blocked
        /// </summary>
        bool IsBlocked { get; set; }
        /// <summary>
        /// Account level (status)
        /// </summary>
        byte Level { get; set; }
        /// <summary>
        /// Account passhash
        /// </summary>
        Passhash Passhash { get; set; }
        /// <summary>
        /// Account email address
        /// </summary>
        EmailAddress EmailAddress { get; set; }
        /// <summary>
        /// Last account access token
        /// </summary>
        Token AccessToken { get; set; }
        /// <summary>
        /// Last account use ip address
        /// </summary>
        IPAddress LastUseIpAddress { get; set; }
    }
}
