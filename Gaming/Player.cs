
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

using OpenSharedLibrary.Credentials;
using OpenSharedLibrary.Extensions;
using System.IO;
using System.Net;

namespace OpenSharedLibrary.Gaming
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player : IPlayer
    {
        /// <summary>
        /// Player data byte array size in bytes
        /// </summary>
        public const int ByteSize = Username.ByteSize + Token.ByteSize + IPEndPointExtension.ByteSize + sizeof(long);

        /// <summary>
        /// Player data byte array size in bytes
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Player username
        /// </summary>
        public Username Username { get; set; }
        /// <summary>
        /// Player connect token
        /// </summary>
        public Token ConnecToken { get; set; }
        /// <summary>
        /// Player remote end point
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; set; }
        /// <summary>
        /// Last player action time
        /// </summary>
        public long LastActionTime { get; set; }

        /// <summary>
        /// Creates a new player instance
        /// </summary>
        public Player() { }
        /// <summary>
        /// Creates a new player instance
        /// </summary>
        public Player(Username username, Token connecToken, IPEndPoint remoteEndPoint, long lastActionTime)
        {
            Username = username;
            ConnecToken = connecToken;
            RemoteEndPoint = remoteEndPoint;
            LastActionTime = lastActionTime;
        }
        /// <summary>
        /// Creates a new player instance
        /// </summary>
        public Player(BinaryReader binaryReader)
        {
            Username = new Username(binaryReader);
            ConnecToken = new Token(binaryReader);
            RemoteEndPoint = IPEndPointExtension.FromBytes(binaryReader);
            LastActionTime = binaryReader.ReadInt64();
        }

        /// <summary>
        /// Converts player data to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            Username.ToBytes(binaryWriter);
            ConnecToken.ToBytes(binaryWriter);
            RemoteEndPoint.ToBytes(binaryWriter);
            binaryWriter.Write(LastActionTime);
        }
    }
}
