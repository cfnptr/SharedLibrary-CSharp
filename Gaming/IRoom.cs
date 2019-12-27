
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
using System;

namespace OpenSharedLibrary.Gaming
{
    /// <summary>
    /// Room interface
    /// </summary>
    public interface IRoom : IComparable, IComparable<IRoom>, IEquatable<IRoom>
    {
        /// <summary>
        /// Room information
        /// </summary>
        RoomInfo Info { get; }

        /// <summary>
        /// Current room player count
        /// </summary>
        int PlayerCount { get; }
        /// <summary>
        /// Maximum room player count
        /// </summary>
        int MaxPlayerCount { get; }

        /// <summary>
        /// Player database
        /// </summary>
        IPlayerDatabase PlayerDatabase { get; }
        /// <summary>
        /// Player array
        /// </summary>
        IPlayerArray Players { get; }

        /// <summary>
        /// Sets a new maximum room player count
        /// </summary>
        void SetMaxPlayerCount(int count);

        /// <summary>
        /// Returns true if player has joined the room
        /// </summary>
        bool JoinPlayer(IAccount account, out RoomInfo roomInfo, out Token connectToken);
        /// <summary>
        /// Returns true if player has disconnected from the room
        /// </summary>
        bool DisconnectPlayer(Username player, int reason);

        /// <summary>
        /// Closes room
        /// </summary>
        void CloseRoom();
    }
}
