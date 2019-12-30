
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

using OpenSharedLibrary.Credentials;
using OpenSharedLibrary.Credentials.Accounts;
using System.Collections.Concurrent;

namespace OpenSharedLibrary.Gaming.Rooms
{
    /// <summary>
    /// Room dictionary class
    /// </summary>
    public class RoomDictionary<TRoom, TAccount> : ConcurrentDictionary<long, TRoom> where TRoom : IRoom where TAccount : IAccount
    {
        /// <summary>
        /// Returns room informations array
        /// </summary>
        public RoomInfo[] GetInfos()
        {
            var index = 0;
            var count = Values.Count;
            var roomInfos = new RoomInfo[count];

            foreach (var room in Values)
            {
                if (index >= count)
                    break;

                roomInfos[index++] = new RoomInfo(room.ID, room.Name);
            }

            return roomInfos;
        }

        /// <summary>
        /// Returns true if player has joined the room
        /// </summary>
        public bool JoinPlayer(long roomId, TAccount account, out RoomInfo roomInfo, out Token connectToken)
        {
            if (!TryGetValue(roomId, out TRoom room))
            {
                roomInfo = default;
                connectToken = null;
                return false;
            }

            return room.JoinPlayer(account, out roomInfo, out connectToken);
        }
    }
}
