using InjectorGames.SharedLibrary.Entities;

namespace InjectorGames.SharedLibrary.Databases
{
    /// <summary>
    /// Database interface
    /// </summary>
    public interface IDatabase<TKey, TValue, TFactory> where TValue : IIdentifiable<TKey>
    {
        /// <summary>
        /// Database item count
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Loads database data
        /// </summary>
        void Load();
        /// <summary>
        /// Unloads database data
        /// </summary>
        void Unload();

        /// <summary>
        /// Returns true if the database contains key
        /// </summary>
        bool ContainsKey(TKey key);
        /// <summary>
        /// Adds a new item to the database
        /// </summary>
        bool TryAdd(TValue value);
        /// <summary>
        /// Updates an existing item in the database
        /// </summary>
        bool TryUpdate(TValue value);
        /// <summary>
        /// Adds new or updates existing item in the database
        /// </summary>
        bool AddOrUpdate(TValue value);
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        bool TryRemove(TKey key);
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        bool TryRemove(TKey key, TFactory factory, out TValue value);
        /// <summary>
        /// Returns an existing item from the database
        /// </summary>
        bool TryGetValue(TKey key, TFactory factory, out TValue value);

        /// <summary>
        /// Removes all items from the database
        /// </summary>
        void Clear();
    }
}
