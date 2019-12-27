
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
using System;
using System.Net;

namespace OpenSharedLibrary.Credentials
{
    /// <summary>
    /// Account container interface
    /// </summary>
    public interface IAccount : IByteArray
    {
        /// <summary>
        /// Account username
        /// </summary>
        Username Username { get; set; }
        /// <summary>
        /// Account passhash
        /// </summary>
        Passhash Passhash { get; set; }
        /// <summary>
        /// Account email address
        /// </summary>
        EmailAddress EmailAddress { get; set; }
        /// <summary>
        /// Last account access token
        /// </summary>
        Token AccessToken { get; set; }
        /// <summary>
        /// Is account has blocked
        /// </summary>
        bool IsBlocked { get; set; }
        /// <summary>
        /// Account level (status)
        /// </summary>
        byte Level { get; set; }
        /// <summary>
        /// Last account use date time
        /// </summary>
        DateTime DateTime { get; set; }
        /// <summary>
        /// Last account use ip address
        /// </summary>
        IPAddress IpAddress { get; set; }
    }
}
