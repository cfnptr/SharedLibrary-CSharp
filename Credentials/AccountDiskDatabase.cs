
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

using OpenSharedLibrary.Containers;
using System;
using System.IO;

namespace OpenSharedLibrary.Credentials
{
    /// <summary>
    /// Account disk database class (thread-safe)
    /// </summary>
    public class AccountDiskDatabase : DiskDatabase<Username, IAccount>, IAccountDiskDatabase
    {
        /// <summary>
        /// Creates a new account disk database instance
        /// </summary>
        public AccountDiskDatabase(string accountFolderPath, IAccountDiskFactory factory) : base(accountFolderPath, factory) { }

        /// <summary>
        /// Returns account data readed from the database (if not readed null)
        /// </summary>
        public override IAccount Read(Username username)
        {
            try
            {
                var array = new byte[factory.ByteArraySize];
                using var memoryStream = new MemoryStream(array);

                lock (locker)
                {
                    var fileStream = new FileStream($"{path}{username}", FileMode.Open, FileAccess.Read);
                    fileStream.CopyTo(memoryStream);
                    fileStream.Close();
                }

                using var binaryReader = new BinaryReader(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return factory.Create(binaryReader);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Writes account data to the database
        /// </summary>
        public override bool Write(Username username, IAccount account)
        {
            try
            {
                var array = new byte[account.ByteArraySize];
                using var memoryStream = new MemoryStream(array);
                using var binaryWriter = new BinaryWriter(memoryStream);

                account.ToBytes(binaryWriter);
                memoryStream.Seek(0, SeekOrigin.Begin);

                lock (locker)
                {
                    var fileStream = new FileStream($"{path}{username}", FileMode.Create, FileAccess.Write);
                    memoryStream.CopyTo(fileStream);
                    fileStream.Close();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
