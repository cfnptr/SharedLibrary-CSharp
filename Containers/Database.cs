
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

namespace OpenSharedLibrary.Containers
{
    /// <summary>
    /// Database abstract base class
    /// </summary>
    public abstract class Database<TKey, TValue> : IDatabase<TKey, TValue>
    {
        /// <summary>
        /// Returns true if the database contains item
        /// </summary>
        public abstract bool Contains(TKey key);
        /// <summary>
        /// Returns item readed from the database (if not readed null)
        /// </summary>
        public abstract TValue Read(TKey key);
        /// <summary>
        /// Writes item to the database (Returns result)
        /// </summary>
        public abstract bool Write(TKey key, TValue value);
    }
}
