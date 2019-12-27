
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
using System.Net;

namespace OpenSharedLibrary.Gaming
{
    /// <summary>
    /// Player array class (thread-safe)
    /// </summary>
    public class PlayerArray : ConcurentCollection<Username, IPlayer, Dictionary<Username, IPlayer>>, IPlayerArray
    {
        /// <summary>
        /// Creates a new player array class instance
        /// </summary>
        public PlayerArray(Dictionary<Username, IPlayer> collection) : base(collection) { }
        /// <summary>
        /// Creates a new player array class instance
        /// </summary>
        public PlayerArray() : base(new Dictionary<Username, IPlayer>()) { }

        /// <summary>
        /// Returns player from the array (null if not exist)
        /// </summary>
        public IPlayer Get(IPEndPoint remoteEndPoint)
        {
            lock (locker)
            {
                foreach (var player in collection.Values)
                {
                    if (remoteEndPoint.Equals(player.RemoteEndPoint))
                        return player;
                }
            }

            return null;
        }
    }
}
