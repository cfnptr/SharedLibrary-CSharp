
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

using System.IO;
using System.Net;

namespace OpenSharedLibrary.Extensions
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
