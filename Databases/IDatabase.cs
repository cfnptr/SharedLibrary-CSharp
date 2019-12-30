
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
using OpenSharedLibrary.Entities;

namespace OpenSharedLibrary.Databases
{
    /// <summary>
    /// Database interface
    /// </summary>
    public interface IDatabase<TKey, TValue, TFactory> where TValue : IIdentifiable<TKey>
    {
        /// <summary>
        /// Database item count
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Loads database data
        /// </summary>
        void Load();
        /// <summary>
        /// Unloads database data
        /// </summary>
        void Unload();

        /// <summary>
        /// Returns true if the database contains key
        /// </summary>
        bool ContainsKey(TKey key);
        /// <summary>
        /// Adds a new item to the database
        /// </summary>
        bool TryAdd(TValue value);
        /// <summary>
        /// Updates an existing item in the database
        /// </summary>
        bool TryUpdate(TValue value);
        /// <summary>
        /// Adds new or updates existing item in the database
        /// </summary>
        bool AddOrUpdate(TValue value);
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        bool TryRemove(TKey key);
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        bool TryRemove(TKey key, TFactory factory, out TValue value);
        /// <summary>
        /// Returns an existing item from the database
        /// </summary>
        bool TryGetValue(TKey key, TFactory factory, out TValue value);

        /// <summary>
        /// Removes all items from the database
        /// </summary>
        void Clear();
    }
}
