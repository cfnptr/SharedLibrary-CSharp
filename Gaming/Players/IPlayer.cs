
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
using OpenSharedLibrary.Credentials;
using OpenSharedLibrary.Entities;
using System;
using System.Net;

namespace OpenSharedLibrary.Gaming.Players
{
    /// <summary>
    /// Player interface
    /// </summary>
    public interface IPlayer : IByteArray, IIdentifiable<long>, INameable<Username>, IComparable, IComparable<IPlayer>, IEquatable<IPlayer>
    {
        /// <summary>
        /// Last player action time in milliseconds
        /// </summary>
        public long LastActionMS { get; set; }
        /// <summary>
        /// Player connect token
        /// </summary>
        public Token ConnecToken { get; set; }
        /// <summary>
        /// Player remote end point
        /// </summary>
        public IPEndPoint RemoteEndPoint { get; set; }
    }
}
