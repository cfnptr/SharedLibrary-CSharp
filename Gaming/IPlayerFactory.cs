
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
using System.Net;

namespace OpenSharedLibrary.Gaming
{
    /// <summary>
    /// Player value factory interface
    /// </summary>
    public interface IPlayerFactory : IFactory<IPlayer>
    {
        /// <summary>
        /// Creates a new player instance
        /// </summary>
        IPlayer Create(Username username, Token connecToken, IPEndPoint remoteEndPoint, long lastActionTime);
    }
}
