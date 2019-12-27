
// Copyright 2019 Nikita Fediuchin (QuantumBranch)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace OpenSharedLibrary.Extensions
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

            if(addressFamily == AddressFamily.InterNetwork)
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
