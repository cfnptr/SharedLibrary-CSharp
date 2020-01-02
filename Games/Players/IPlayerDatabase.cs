using InjectorGames.SharedLibrary.Credentials;
using InjectorGames.SharedLibrary.Databases;

namespace InjectorGames.SharedLibrary.Games.Players
{
    /// <summary>
    /// Player database interface
    /// </summary>
    public interface IPlayerDatabase<TPlayer, TFactory> : INameDatabase<Username, long, TPlayer, TFactory> where TPlayer : IPlayer { }
}
