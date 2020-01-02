using InjectorGames.SharedLibrary.Entities;

namespace InjectorGames.SharedLibrary.Databases
{
    /// <summary>
    /// Name database interface
    /// </summary>
    public interface INameDatabase<TName, TKey, TValue, TFactory> : IDatabase<TKey, TValue, TFactory> where TValue : INameable<TName>, IIdentifiable<TKey>
    {
        /// <summary>
        /// Returns true if the database contains key
        /// </summary>
        bool ContainsKey(TName name);
        /// <summary>
        /// Removes item from the database
        /// </summary>
        bool TryRemove(TName name);
        /// <summary>
        /// Removes item from the database
        /// </summary>
        bool TryRemove(TName name, TFactory factory, out TValue value);
        /// <summary>
        /// Returns item from the database
        /// </summary>
        bool TryGetValue(TName name, TFactory factory, out TValue value);
    }
}
