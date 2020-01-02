using InjectorGames.SharedLibrary.Databases;

namespace InjectorGames.SharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account database interface
    /// </summary>
    public interface IAccountDatabase<TAccount, TFactory> : INameDatabase<Username, long, TAccount, TFactory> where TAccount : IAccount { }
}
