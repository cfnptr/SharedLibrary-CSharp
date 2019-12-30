
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

namespace OpenSharedLibrary.Gaming.Rooms
{
    /// <summary>
    /// Room information container structure
    /// </summary>
    public struct RoomInfo
    {
        /// <summary>
        /// Unique room identifier
        /// </summary>
        public long id;
        /// <summary>
        /// Room name
        /// </summary>
        public string name;

        /// <summary>
        /// Creates a new room container instance
        /// </summary>
        public RoomInfo(long id, string name)
        {
            this.id = id;
            this.name = name;
        }
        /// <summary>
        /// Creates a new room container instance
        /// </summary>
        public RoomInfo(string serialized)
        {
            var values = serialized.Split(';');

            if (values.Length != 2)
                throw new ArgumentException();

            id = long.Parse(values[0]);
            name = values[1];
        }

        /// <summary>
        /// Returns room information string
        /// </summary>
        public override string ToString()
        {
            return $"<{id}, {name}>";
        }

        /// <summary>
        /// Serializes room information container
        /// </summary>
        public string Serialize()
        {
            return $"{id};{name}";
        }
    }
}
