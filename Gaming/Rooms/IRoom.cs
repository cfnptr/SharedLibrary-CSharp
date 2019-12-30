
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
using OpenSharedLibrary.Entities;
using System;

namespace OpenSharedLibrary.Gaming.Rooms
{
    /// <summary>
    /// Room interface
    /// </summary>
    public interface IRoom : IIdentifiable<long>, IComparable, IComparable<IRoom>, IEquatable<IRoom>
    {
        /// <summary>
        /// Room name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Maximum room player count
        /// </summary>
        int MaxPlayerCount { get; set; }

        /// <summary>
        /// Current room player count
        /// </summary>
        int PlayerCount { get; }

        /// <summary>
        /// Returns true if player has joined the room
        /// </summary>
        bool JoinPlayer(IAccount account, out RoomInfo roomInfo, out Token connectToken);
        /// <summary>
        /// Returns true if player has disconnected from the room
        /// </summary>
        bool DisconnectPlayer(long id, int reason);
    }
}
