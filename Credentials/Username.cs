
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
using System.IO;
using System.Text;

namespace OpenSharedLibrary.Credentials
{
    /// <summary>
    /// Alphanumeric lowercase username container (with "_")
    /// </summary>
    public class Username : IComparable, IComparable<Username>, IEquatable<Username>, IByteArray
    {
        /// <summary>
        /// Username value size in bytes
        /// </summary>
        public const int ByteSize = 16;
        /// <summary>
        /// Minimum username length
        /// </summary>
        public const int MinLength = 4;
        /// <summary>
        /// Maximum username length
        /// </summary>
        public const int MaxLength = 16;

        /// <summary>
        /// Username value size in bytes
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Username string value
        /// </summary>
        protected readonly string value;

        /// <summary>
        /// Creates a new username container class instance
        /// </summary>
        public Username(string value)
        {
            if (!IsValidLength(value.Length))
                throw new ArgumentException("Invalid username length");
            if (!IsValidLetters(value))
                throw new ArgumentException("Invalid username letters");

            this.value = value;
        }
        /// <summary>
        /// Creates a new username container class instance
        /// </summary>
        public Username(BinaryReader binaryReader)
        {
            var username = Encoding.ASCII.GetString(binaryReader.ReadBytes(ByteSize));

            for (int i = 0; i < ByteSize; i++)
            {
                if (username[i] == '\0')
                {
                    if (!IsValidLength(i))
                        throw new ArgumentException("Invalid username length");

                    username = username.Substring(0, i);

                    if (!IsValidLetters(username))
                        throw new ArgumentException("Invalid username letters");

                    value = username;
                    return;
                }
            };
        }

        /// <summary>
        /// Returns true if the username is equal to the object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is Username username)
                return value.Equals(username.value);
            else
                return false;
        }
        /// <summary>
        /// Returns username hash code 
        /// </summary>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        /// <summary>
        /// Returns username string value
        /// </summary>
        public override string ToString()
        {
            return value;
        }

        /// <summary>
        /// Compares username to the object
        /// </summary>
        public int CompareTo(object obj)
        {
            return value.CompareTo(obj);
        }
        /// <summary>
        /// Compares two usernames
        /// </summary>
        public int CompareTo(Username other)
        {
            return value.CompareTo(other.value);
        }
        /// <summary>
        /// Returns true if two usernames is equal
        /// </summary>
        public bool Equals(Username other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Converts username value to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            var array = Encoding.ASCII.GetBytes(value);
            binaryWriter.Write(array);

            if(array.Length < ByteSize)
                binaryWriter.Write(new byte[ByteSize - array.Length]);
        }

        /// <summary>
        /// Returns true if the username has valid length
        /// </summary>
        public static bool IsValidLength(int length)
        {
            return length >= MinLength && length <= MaxLength;
        }

        /// <summary>
        /// Returns true if the username has valid letters
        /// </summary>
        public static bool IsValidLetters(string username)
        {
            for (int i = 0; i < username.Length; i++)
            {
                var letter = username[i];

                if (!((letter > 47 && letter < 58) || (letter > 96 && letter < 123) || letter == 95))
                    return false;
            }

            return true;
        }

        public static bool operator ==(Username a, Username b) { return a.value == b.value; }
        public static bool operator !=(Username a, Username b) { return a.value != b.value; }
        public static bool operator ==(Username a, string b) { return a.value == b; }
        public static bool operator !=(Username a, string b) { return a.value != b; }
        public static bool operator ==(string a, Username b) { return a == b.value; }
        public static bool operator !=(string a, Username b) { return a != b.value; }

        public static implicit operator string(Username v) { return v.value; }
        public static implicit operator Username(string v) { return new Username(v); }
    }
}
