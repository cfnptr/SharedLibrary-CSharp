
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
using System.Numerics;

namespace OpenSharedLibrary
{
    /// <summary>
    /// Three-dimensional array container interface
    /// </summary>
    public interface IArray3<T>
    {
        /// <summary>
        /// Length of an array
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Array size along X-axis
        /// </summary>
        int SizeX { get; }
        /// <summary>
        /// Array size along Y-axis
        /// </summary>
        int SizeY { get; }
        /// <summary>
        /// Array size along Z-axis
        /// </summary>
        int SizeZ { get; }

        /// <summary>
        /// Returns value from an array
        /// </summary>
        T Get(int x, int y, int z);
        /// <summary>
        /// Returns value from an array
        /// </summary>
        T Get(Vector3 position);

        /// <summary>
        /// Sets value to an array
        /// </summary>
        void Set(int x, int y, int z, T value);
        /// <summary>
        /// Sets value to an array
        /// </summary>
        void Set(Vector3 position, T value);

        /// <summary>
        /// Returns this array items
        /// </summary>
        T[][][] GetItems();

        /// <summary>
        /// Returns fragment from an three-dimensional array
        /// </summary>
        T[][][] GetFragment(int sizeX, int sizeY, int sizeZ);
        /// <summary>
        /// Returns fragment from an three-dimensional array
        /// </summary>
        T[][][] GetFragment(int sizeX, int sizeY, int sizeZ, int offsetX, int offsetY, int offsetZ);

        /// <summary>
        /// Copies this three-dimensional array to another
        /// </summary>
        void CopyTo(IArray3<T> array);
        /// <summary>
        /// Copies this three-dimensional array to another
        /// </summary>
        void CopyTo(IArray3<T> array, int offsetX, int offsetY, int offsetZ);
    }
}
