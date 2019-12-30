
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
using System.Collections.Concurrent;
using System.Net;

namespace OpenSharedLibrary.Gaming.Players
{
    /// <summary>
    /// Player dictionary class
    /// </summary>
    public class PlayerDictionary<T> : ConcurrentDictionary<long, T> where T : IPlayer
    {
        /// <summary>
        /// Attempts to get player from the concurrent dictionary
        /// </summary>
        public bool TryGetValue(Username name, out T player)
        {
            foreach (var value in Values)
            {
                if (name.Equals(value.Name))
                {
                    player = value;
                    return true;
                }
            }

            player = default;
            return false;
        }
        /// <summary>
        /// Attempts to get player from the concurrent dictionary
        /// </summary>
        public bool TryGetValue(IPEndPoint remoteEndPoint, out T player)
        {
            foreach (var value in Values)
            {
                if (remoteEndPoint.Equals(value.RemoteEndPoint))
                {
                    player = value;
                    return true;
                }  
            }

            player = default;
            return false;
        }
    }
}
