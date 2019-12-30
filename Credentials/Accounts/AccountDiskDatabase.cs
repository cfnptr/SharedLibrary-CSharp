
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

using OpenSharedLibrary.Collections.Bytes;
using OpenSharedLibrary.Databases;
using System.IO;

namespace OpenSharedLibrary.Credentials.Accounts
{
    /// <summary>
    /// Account disk database class
    /// </summary>
    public class AccountDiskDatabase<TAccount, TFactory> : NameDiskDatabase<Username, long, TAccount, TFactory>, IAccountDatabase<TAccount, TFactory>
        where TAccount : IAccount
        where TFactory : IByteArrayFactory<TAccount>
    {
        /// <summary>
        /// Creates a new account disk database class instance
        /// </summary>
        public AccountDiskDatabase(string path, bool useCompression) : base(path, useCompression) { }

        /// <summary>
        /// Loads database names
        /// </summary>
        public override void LoadNames()
        {
            lock (locker)
            {
                if (!File.Exists($"{path}names"))
                    return;

                var array = File.ReadAllBytes($"{path}names");
                var count = names.Count / (Username.ByteSize + sizeof(long));
                using var memoryStream = new MemoryStream(array);
                using var binaryReader = new BinaryReader(memoryStream);

                for (int i = 0; i < count; i++)
                {
                    var name = new Username(binaryReader);
                    var id = binaryReader.ReadInt64();
                    names.Add(name, id);
                }
            }
        }
        /// <summary>
        /// Unloads database names
        /// </summary>
        public override void UnloadNames()
        {
            lock (locker)
            {
                var array = new byte[names.Count * (Username.ByteSize + sizeof(long))];
                using var memoryStream = new MemoryStream(array);
                using var binaryWriter = new BinaryWriter(memoryStream);

                foreach (var name in names)
                {
                    name.Key.ToBytes(binaryWriter);
                    binaryWriter.Write(name.Value);
                }

                var fileStream = new FileStream($"{path}names", FileMode.Create, FileAccess.Write);
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.CopyTo(fileStream);
                fileStream.Close();
            }
        }
    }
}
