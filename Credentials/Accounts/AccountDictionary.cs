using System.Collections.Concurrent;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account concurrent dictionary class
    /// </summary>
    public class AccountDictionary<T> : ConcurrentDictionary<long, T> where T : IAccount
    {
        /// <summary>
        /// Attempts to get player from the concurrent dictionary
        /// </summary>
        public bool TryGetValue(Username name, out T account)
        {
            foreach (var value in Values)
            {
                if (name.Equals(value.Name))
                {
                    account = value;
                    return true;
                }
            }

            account = default;
            return false;
        }
    }
}
