
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
using OpenSharedLibrary.Credentials;
using System.Collections.Generic;

namespace OpenSharedLibrary.Gaming
{
    /// <summary>
    /// Room array class (thread-safe)
    /// </summary>
    public class RoomArray : ConcurentCollection<int, IRoom, SortedList<int, IRoom>>, IRoomArray
    {
        /// <summary>
        /// Creates a new room array class instance
        /// </summary>
        public RoomArray(SortedList<int, IRoom> collection) : base(collection) { }
        /// <summary>
        /// Creates a new room array class instance
        /// </summary>
        public RoomArray() : base(new SortedList<int, IRoom>()) { }

        /// <summary>
        /// Returns true if player has joined the room
        /// </summary>
        public bool JoinPlayer(int roomId, IAccount account, out RoomInfo roomInfo, out Token connectToken)
        {
            lock (locker)
            {
                if (!collection.TryGetValue(roomId, out IRoom room))
                {
                    roomInfo = default;
                    connectToken = null;
                    return false;
                }

                return room.JoinPlayer(account, out roomInfo, out connectToken);
            }
        }

        /// <summary>
        /// Returns room array informations
        /// </summary>
        public RoomInfo[] GetInfos()
        {
            lock (locker)
            {
                var index = 0;
                var roomInfos = new RoomInfo[collection.Count];

                foreach (var room in collection.Values)
                    roomInfos[index++] = room.Info;

                return roomInfos;
            }
        }
    }
}
