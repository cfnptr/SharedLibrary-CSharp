
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
using System.Numerics;
using System.Text;

namespace OpenSharedLibrary.Credentials
{
    /// <summary>
    /// Password hash container
    /// </summary>
    public class Passhash : IComparable<Passhash>, IEquatable<Passhash>, IByteArray
    {
        /// <summary>
        /// Passhash byte array size
        /// </summary>
        public const int ByteSize = 64;
        /// <summary>
        /// Minimum password length
        /// </summary>
        public const int MinPasswordLength = 6;

        /// <summary>
        /// Passhash byte array size
        /// </summary>
        public int ByteArraySize => ByteSize;

        /// <summary>
        /// Passhash big integer value
        /// </summary>
        protected BigInteger value;

        /// <summary>
        /// Creates a new passhash container class instance
        /// </summary>
        public Passhash(BigInteger value)
        {
            var bytes = value.ToByteArray();

            if (!IsValidLength(bytes.Length))
                throw new ArgumentException("Invalid passhash length");

            this.value = value;
        }
        /// <summary>
        /// Creates a new passhash container class instance
        /// </summary>
        public Passhash(byte[] value)
        {
            if (!IsValidLength(value.Length))
                throw new ArgumentException("Invalid passhash length");

            this.value = new BigInteger(value);
        }
        /// <summary>
        /// Creates a new passhash container class instance
        /// </summary>
        public Passhash(BinaryReader binaryReader)
        {
            value = new BigInteger(binaryReader.ReadBytes(ByteSize));
        }
        /// <summary>
        /// Creates a new passhash container class instance
        /// </summary>
        public Passhash(string base64)
        {
            var bytes = Convert.FromBase64String(base64);

            if (!IsValidLength(bytes.Length))
                throw new ArgumentException("Invalid passhash length");
            
            value = new BigInteger(bytes);
        }

        /// <summary>
        /// Returns true if passhash is equal to the object
        /// </summary>
        public override bool Equals(object obj)
        {
            return value.Equals((Passhash)obj);
        }
        /// <summary>
        /// Returns passhash hash code 
        /// </summary>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Compares two passhashes
        /// </summary>
        public int CompareTo(Passhash other)
        {
            return value.CompareTo(other.value);
        }
        /// <summary>
        /// Returns true if passhashes is equal
        /// </summary>
        public bool Equals(Passhash other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Converts passhash value to the base 64 string value
        /// </summary>
        public string ToBase64()
        {
            var bytes = value.ToByteArray();
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts passhash value to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(value.ToByteArray());
        }

        /// <summary>
        /// Returns true if the passhash has valid length
        /// </summary>
        public static bool IsValidLength(int length)
        {
            return length == ByteSize;
        }

        /// <summary>
        /// Returns true if the password has valid length
        /// </summary>
        public static bool IsPasswordValidLength(int length)
        {
            return length >= MinPasswordLength;
        }

        /// <summary>
        /// Creaetes a new passhash from the password string value
        /// </summary>
        public static Passhash PasswordToPasshash(string password, int iterationCount = ushort.MaxValue)
        {
            if (!IsPasswordValidLength(password.Length))
                throw new ArgumentException("Invalid password length");

            var hash = Cryptographer.ToSHA512(password, Encoding.UTF8, iterationCount);
            return new Passhash(hash);
        }

        public static bool operator ==(Passhash a, Passhash b) { return a.value.Equals(b); }
        public static bool operator !=(Passhash a, Passhash b) { return !a.value.Equals(b); }
        public static bool operator ==(Passhash a, BigInteger b) { return a.value.Equals(b); }
        public static bool operator !=(Passhash a, BigInteger b) { return !a.value.Equals(b); }
        public static bool operator ==(BigInteger a, Passhash b) { return a.Equals(b); }
        public static bool operator !=(BigInteger a, Passhash b) { return !a.Equals(b); }

        public static implicit operator BigInteger(Passhash v) { return v.value; }
        public static implicit operator Passhash(BigInteger v) { return new Passhash(v); }
        public static implicit operator byte[](Passhash v) { return v.value.ToByteArray(); }
        public static implicit operator Passhash(byte[] v) { return new Passhash(v); }
    }
}
