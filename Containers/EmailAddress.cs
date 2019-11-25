
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
using System.Net.Mail;
using System.Text;

namespace QuantumBranch.OpenSharedLibrary
{
    /// <summary>
    /// Email adderss container
    /// </summary>
    public class EmailAddress : MailAddress
    {
        /// <summary>
        /// Email address byte array size (1 size + 255 address)
        /// </summary>
        public const int ByteSize = 256;
        /// <summary>
        /// Minimum length of the email address (custom limitation)
        /// </summary>
        public const int MinLength = 5;
        /// <summary>
        /// Maximum length of the email address (custom limitation)
        /// </summary>
        public const int MaxLength = 255;

        /// <summary>
        /// Creates a new email address container class instance
        /// </summary>
        public EmailAddress(string address) : base(address)
        {
            if (address != Address)
                throw new ArgumentException("Invalid email address", nameof(address));
            if (!IsValidLength(address.Length))
                throw new ArgumentException("Invalid email address length", nameof(address));
        }
        /// <summary>
        /// Creates a new email address container class instance
        /// </summary>
        public EmailAddress(string address, string displayName) : base(address, displayName)
        {
            if (address != Address)
                throw new ArgumentException("Invalid email address", nameof(address));
            if (!IsValidLength(address.Length))
                throw new ArgumentException("Invalid email address length", nameof(address));
        }
        /// <summary>
        /// Creates a new email address container class instance
        /// </summary>
        public EmailAddress(string address, string displayName, Encoding displayNameEncoding) : base(address, displayName, displayNameEncoding)
        {
            if (address != Address)
                throw new ArgumentException("Invalid email address", nameof(address));
            if (!IsValidLength(address.Length))
                throw new ArgumentException("Invalid email address length", nameof(address));
        }

        /// <summary>
        /// Converts email address value to the byte array
        /// </summary>
        public void ToBytes(byte[] array, int index)
        {
            var addressLength = Address.Length;
            array[index] = (byte)addressLength;
            Encoding.ASCII.GetBytes(Address, 0, Address.Length, array, index + 1);
        }
        /// <summary>
        /// Converts email address value to the byte array
        /// </summary>
        public byte[] ToBytes()
        {
            var bytes = new byte[ByteSize];
            ToBytes(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// Returns true if the email address has valid length
        /// </summary>
        public static bool IsValidLength(int length)
        {
            return length >= MinLength && length <= MaxLength;
        }

        /// <summary>
        /// Creates a new email address from the byte array
        /// </summary>
        public static EmailAddress FromBytes(byte[] array, int index)
        {
            var length = array[index];
            var address = Encoding.ASCII.GetString(array, index + 1, length);
            return new EmailAddress(address);
        }
    }
}
