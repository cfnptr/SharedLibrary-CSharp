
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

namespace OpenSharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account factory class
    /// </summary>
    public class AccountFactory : IAccountFactory<IAccount>
    {
        /// <summary>
        /// Account data byte array size
        /// </summary>
        public int ByteArraySize => Account.ByteSize;

        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create()
        {
            return new Account();
        }
        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create(BinaryReader binaryReader)
        {
            return new Account(binaryReader);
        }
        /// <summary>
        /// Creates a new account instance
        /// </summary>
        public IAccount Create(long id, bool isBlocked, byte level, Username name, Passhash passhash, EmailAddress emailAddress, Token accessToken, IPAddress ipAddress)
        {
            return new Account(id, isBlocked, level, name, passhash, emailAddress, accessToken, ipAddress);
        }
    }
}
