using System.IO;

namespace InjectorGames.SharedLibrary.Collections.Bytes
{
    /// <summary>
    /// Byte array interface
    /// </summary>
    public interface IByteArray
    {
        /// <summary>
        /// Class data byte array size in bytes
        /// </summary>
        int ByteArraySize { get; }

        /// <summary>
        /// Converts class data to the byte array
        /// </summary>
        void ToBytes(BinaryWriter binaryWriter);
    }
}
