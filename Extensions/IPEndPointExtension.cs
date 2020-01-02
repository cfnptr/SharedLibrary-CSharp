using System.IO;
using System.Net;

namespace InjectorGames.SharedLibrary.Extensions
{
    /// <summary>
    /// IPEndPoint extension container class
    /// </summary>
    public static class IPEndPointExtension
    {
        /// <summary>
        /// IPEndPoint byte array size in the bytes
        /// </summary>
        public const int ByteSize = IPAddressExtension.ByteSize + sizeof(int);

        /// <summary>
        /// Converts IPEndPoint value to the byte array
        /// </summary>
        public static void ToBytes(this IPEndPoint ipEndPoint, BinaryWriter binaryWrite)
        {
            ipEndPoint.ToBytes(binaryWrite);
            binaryWrite.Write(ipEndPoint.Port);
        }

        /// <summary>
        /// Creates a new IPEndPoint from the byte array
        /// </summary>
        public static IPEndPoint FromBytes(BinaryReader binaryReader)
        {
            return new IPEndPoint(IPAddressExtension.FromBytes(binaryReader), binaryReader.ReadInt32());
        }
    }
}
