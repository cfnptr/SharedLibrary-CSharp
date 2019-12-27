
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

namespace OpenSharedLibrary.Containers
{
    /// <summary>
    /// Disk database abstract base class (thread-safe)
    /// </summary>
    public abstract class DiskDatabase<TKey, TValue> : Database<TKey, TValue>, IDiskDatabase<TKey, TValue>
    {
        /// <summary>
        /// Database locker
        /// </summary>
        protected readonly object locker;

        /// <summary>
        /// Database directory path
        /// </summary>
        protected readonly string path;
        /// <summary>
        /// Database value factory
        /// </summary>
        protected readonly IDiskFactory<TValue> factory;

        /// <summary>
        /// Database directory path
        /// </summary>
        public string Path => path;
        /// <summary>
        /// Database value factory
        /// </summary>
        public IDiskFactory<TValue> Factory => factory;

        /// <summary>
        /// Creates a new disk database abstract class instance
        /// </summary>
        public DiskDatabase(string path, IDiskFactory<TValue> factory)
        {
            locker = new object();

            this.path = path ?? throw new ArgumentNullException();
            this.factory = factory ?? throw new ArgumentNullException();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Returns true if the database contains item
        /// </summary>
        public override bool Contains(TKey key)
        {
            lock(locker)
                return File.Exists($"{path}{key}");
        }
    }
}
