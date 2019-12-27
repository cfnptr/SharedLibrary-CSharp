
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

using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace OpenSharedLibrary.Logging
{
    /// <summary>
    /// Smart file logger class
    /// </summary>
    public class SmartFileLogger : FileLogger
    {
        /// <summary>
        /// Fatal error SMTP server hostname
        /// </summary>
        protected readonly string smtpHost;
        /// <summary>
        /// Fatal error SMTP server port
        /// </summary>
        protected readonly int smtpPort;
        /// <summary>
        /// Fatal error SMTP from address
        /// </summary>
        protected readonly string fromAddress;
        /// <summary>
        /// Fatal error SMTP to address
        /// </summary>
        protected readonly string toAddress;
        /// <summary>
        /// Fatal error SMTP from password
        /// </summary>
        protected readonly ICredentialsByHost smtpCredentials;

        /// <summary>
        /// Creates a new smart file stream logger class instance
        /// </summary>
        public SmartFileLogger(string smtpHost, int smtpPort, string fromAddress, string toAddress, ICredentialsByHost smtpCredentials, Stopwatch timer, LogType level, string logFolderPath) : base(timer, level, logFolderPath)
        {
            this.smtpHost = smtpHost;
            this.smtpPort = smtpPort;
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.smtpCredentials = smtpCredentials;
        }

        /// <summary>
        /// On fatal log message event
        /// </summary>
        protected override void OnFatalLog(string message)
        {
            // TODO: Use MailKit library
            var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = smtpCredentials,
            };

            var subject = "Logged Fatal Exception";
            smtpClient.Send(fromAddress, toAddress, subject, message);
        }
    }
}
