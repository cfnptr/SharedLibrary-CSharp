using InjectorGames.SharedLibrary.Collections.Bytes;
using InjectorGames.SharedLibrary.Entities;

namespace InjectorGames.SharedLibrary.Databases
{
    /// <summary>
    /// Name disk database interface
    /// </summary>
    public interface INameDiskDatabase<TName, TKey, TValue, TFactory> : INameDatabase<TName, TKey, TValue, TFactory>, IDiskDatabase<TKey, TValue, TFactory>
        where TValue : IByteArray, INameable<TName>, IIdentifiable<TKey>
        where TFactory : IByteArrayFactory<TValue>
    { }
}
