using InjectorGames.SharedLibrary.Entities;
using System.IO;

namespace InjectorGames.SharedLibrary.Collections.Bytes
{
    /// <summary>
    /// Byte array factory interface
    /// </summary>
    public interface IByteArrayFactory<T> : IFactory<T> where T : IByteArray
    {
        /// <summary>
        /// Item data byte array size
        /// </summary>
        int ByteArraySize { get; }

        /// <summary>
        /// Creates a new item instance
        /// </summary>
        T Create(BinaryReader binaryReader);
    }
}
