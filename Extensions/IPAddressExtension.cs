using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace InjectorGames.SharedLibrary.Extensions
{
    /// <summary>
    /// IPAddress extension container class
    /// </summary>
    public static class IPAddressExtension
    {
        /// <summary>
        /// IPv4 address byte array size in the bytes
        /// </summary>
        public const int IPv4ByteSize = 4;
        /// <summary>
        /// IPv6 address byte array size in the bytes
        /// </summary>
        public const int IPv6ByteSize = 16;
        /// <summary>
        /// IPAddress byte array size in the bytes
        /// </summary>
        public const int ByteSize = IPv6ByteSize;

        /// <summary>
        /// Converts IPAddress value to the byte array
        /// </summary>
        public static void ToBytes(this IPAddress address, BinaryWriter binaryWriter)
        {
            var addressFamily = address.AddressFamily;

            if (addressFamily == AddressFamily.InterNetwork)
            {
                var bytes = address.GetAddressBytes();
                binaryWriter.Write((byte)addressFamily);
                binaryWriter.Write(bytes, 0, IPv4ByteSize);
                binaryWriter.Seek(ByteSize - IPv4ByteSize, SeekOrigin.Current);
            }
            else if (addressFamily == AddressFamily.InterNetworkV6)
            {
                var bytes = address.GetAddressBytes();
                binaryWriter.Write((byte)addressFamily);
                binaryWriter.Write(bytes, 0, IPv6ByteSize);
            }
            else
            {
                throw new InvalidOperationException("Unsupported IP address family");
            }
        }

        /// <summary>
        /// Creates a new IP address from the byte array
        /// </summary>
        public static IPAddress FromBytes(BinaryReader binaryReader)
        {
            var addressFamily = (AddressFamily)binaryReader.ReadByte();

            if (addressFamily == AddressFamily.InterNetwork)
            {
                var bytes = binaryReader.ReadBytes(IPv4ByteSize);
                binaryReader.BaseStream.Seek(ByteSize - IPv4ByteSize, SeekOrigin.Current);
                return new IPAddress(bytes);
            }
            else if (addressFamily == AddressFamily.InterNetworkV6)
            {
                var bytes = binaryReader.ReadBytes(IPv6ByteSize);
                return new IPAddress(bytes);
            }
            else
            {
                throw new InvalidOperationException("Unsupported IP address family");
            }
        }
    }
}
