
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
using System.Collections.Generic;

namespace OpenSharedLibrary.Containers
{
    /// <summary>
    /// Concurent collection base class (thread-safe)
    /// </summary>
    public class ConcurentCollection<TKey, TValue, TCollection> : IConcurentCollection<TKey, TValue> where TCollection : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Concurent array locker
        /// </summary>
        protected readonly object locker;
        /// <summary>
        /// Concurent array instance
        /// </summary>
        protected readonly TCollection collection;

        /// <summary>
        /// Creates a new concurent array abstract base class instance
        /// </summary>
        public ConcurentCollection(TCollection collection)
        {
            locker = new object();
            this.collection = collection;
        }

        /// <summary>
        /// Returns true if collection contains item
        /// </summary>
        public bool Contains(TKey key)
        {
            lock (locker)
                return collection.ContainsKey(key);
        }
        /// <summary>
        /// Adds a new item to the array
        /// </summary>
        public bool Add(TKey key, TValue value)
        {
            lock (locker)
                return collection.TryAdd(key, value);
        }
        /// <summary>
        /// Returns item from the collection (null if not exist)
        /// </summary>
        public TValue Get(TKey key)
        {
            lock (locker)
            {
                if (collection.TryGetValue(key, out TValue player))
                    return player;
            }

            return default;
        }

        /// <summary>
        /// Removes item from the array
        /// </summary>
        public bool Remove(TKey key)
        {
            lock (locker)
                return collection.Remove(key);
        }
        /// <summary>
        /// Removes item from the collection
        /// </summary>
        public bool Remove(TKey key, out TValue value)
        {
            lock (locker)
            {
                if (!collection.TryGetValue(key, out value))
                    return false;

                collection.Remove(key);
                return true;
            }
        }

        /// <summary>
        /// Removes all collection items
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            lock (locker)
                collection.Clear();
        }
        
        /// <summary>
        /// Returns true if action has performed
        /// </summary>
        public bool Lock(TKey key, Action<TValue> onAction)
        {
            lock (locker)
            {
                if (!collection.TryGetValue(key, out TValue value))
                    return false;

                try { onAction(value); }
                catch { return false; }
            }

            return true;
        }
        /// <summary>
        /// Returns true if action has performed (for each room)
        /// </summary>
        public bool Lock(Action<TValue> onAction)
        {
            lock (locker)
            {
                try
                {
                    foreach (var value in collection.Values)
                        onAction(value);
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
    }
}
