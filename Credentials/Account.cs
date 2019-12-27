
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

using OpenSharedLibrary.Extensions;
using System;
using System.IO;
using System.Net;

namespace OpenSharedLibrary.Credentials
{
    /// <summary>
    /// Account container class
    /// </summary>
    public class Account :  IAccount
    {
        /// <summary>
        /// Account data container byte array size
        /// </summary>
        public const int ByteSize = Username.ByteSize + Passhash.ByteSize + EmailAddress.ByteSize + Token.ByteSize + sizeof(byte) * 2 + sizeof(long) + IPAddressExtension.ByteSize;

        /// <summary>
        /// Account data container byte array size
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Account passhash
        /// </summary>
        public Username Username { get; set; }
        /// <summary>
        /// Account passhash
        /// </summary>
        public Passhash Passhash { get; set; }
        /// <summary>
        /// Account email address
        /// </summary>
        public EmailAddress EmailAddress { get; set; }
        /// <summary>
        /// Last account access token
        /// </summary>
        public Token AccessToken { get; set; }
        /// <summary>
        /// Is account has blocked
        /// </summary>
        public bool IsBlocked { get; set; }
        /// <summary>
        /// Account level (status)
        /// </summary>
        public byte Level { get; set; }
        /// <summary>
        /// Last account use date time
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Last account use ip address
        /// </summary>
        public IPAddress IpAddress { get; set; }

        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account() { }
        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account(Username username, Passhash passhash, EmailAddress emailAddress, Token accessToken, bool isBlocked, byte level, DateTime dateTime, IPAddress ipAddress)
        {
            Username = username;
            Passhash = passhash;
            EmailAddress = emailAddress;
            AccessToken = accessToken;
            IsBlocked = isBlocked;
            Level = level;
            DateTime = dateTime;
            IpAddress = ipAddress;
        }
        /// <summary>
        /// Creates a new account data container class instance
        /// </summary>
        public Account(BinaryReader binaryReader)
        {
            Username = new Username(binaryReader);
            Passhash = new Passhash(binaryReader);
            EmailAddress = new EmailAddress(binaryReader);
            AccessToken = new Token(binaryReader);
            IsBlocked = binaryReader.ReadBoolean();
            Level = binaryReader.ReadByte();
            DateTime = new DateTime(binaryReader.ReadInt64());
            IpAddress = IPAddressExtension.FromBytes(binaryReader);
        }

        /// <summary>
        /// Converts account data container to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            Username.ToBytes(binaryWriter);
            Passhash.ToBytes(binaryWriter);
            EmailAddress.ToBytes(binaryWriter);
            AccessToken.ToBytes(binaryWriter);
            binaryWriter.Write(IsBlocked);
            binaryWriter.Write(Level);
            binaryWriter.Write(DateTime.Ticks);
            IpAddress.ToBytes(binaryWriter);
        }

        /// <summary>
        /// Sets blocked flag and overrides acces token
        /// </summary>
        public void BlockAccount()
        {
            IsBlocked = true;
            AccessToken = Token.Create();
        }
    }
}
