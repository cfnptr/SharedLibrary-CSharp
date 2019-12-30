
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
    /// Name database interface
    /// </summary>
    public interface INameDatabase<TName, TKey, TValue, TFactory> : IDatabase<TKey, TValue, TFactory> where TValue : INameable<TName>, IIdentifiable<TKey>
    {
        /// <summary>
        /// Returns true if the database contains key
        /// </summary>
        bool ContainsKey(TName name);
        /// <summary>
        /// Removes item from the database
        /// </summary>
        bool TryRemove(TName name);
        /// <summary>
        /// Removes item from the database
        /// </summary>
        bool TryRemove(TName name, TFactory factory, out TValue value);
        /// <summary>
        /// Returns item from the database
        /// </summary>
        bool TryGetValue(TName name, TFactory factory, out TValue value);
    }
}
