using InjectorGames.SharedLibrary.Collections.Bytes;
using InjectorGames.SharedLibrary.Entities;

namespace InjectorGames.SharedLibrary.Databases
{
    /// <summary>
    /// Disk database interface
    /// </summary>
    public interface IDiskDatabase<TKey, TValue, TFactory> : IDatabase<TKey, TValue, TFactory>
        where TValue : IByteArray, IIdentifiable<TKey>
        where TFactory : IByteArrayFactory<TValue>
    {
        /// <summary>
        /// Database directory path
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Use GZip file compression
        /// </summary>
        bool UseCompression { get; }
    }
}
