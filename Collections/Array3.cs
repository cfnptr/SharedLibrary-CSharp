using System;
using System.Numerics;

namespace InjectorGames.SharedLibrary.Collections
{
    /// <summary>
    /// Three-dimensional array container class
    /// </summary>
    public class Array3<T> : IArray3<T>
    {
        /// <summary>
        /// Number of items in an array
        /// </summary>
        public int Length => length;

        /// <summary>
        /// Array size along X-axis
        /// </summary>
        public int SizeX => sizeX;
        /// <summary>
        /// Array size along Y-axis
        /// </summary>
        public int SizeY => sizeY;
        /// <summary>
        /// Array size along Z-axis
        /// </summary>
        public int SizeZ => sizeZ;

        /// <summary>
        /// Number of items in an array
        /// </summary>
        protected readonly int length;
        /// <summary>
        /// Array size along axis
        /// </summary>
        protected readonly int sizeX, sizeY, sizeZ;

        /// <summary>
        /// Array instance
        /// </summary>
        protected readonly T[][][] items;

        /// <summary>
        /// Creates a new array class instance
        /// </summary>
        public Array3(T[][][] items)
        {
            sizeY = items.Length;
            sizeZ = items[0].Length;
            sizeX = items[0][0].Length;
            length = sizeX * sizeY * sizeZ;
            this.items = items;
        }


        /// <summary>
        /// Creates a new array class instance
        /// </summary>
        public Array3(int sizeX, int sizeY, int sizeZ)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sizeZ = sizeZ;
            length = sizeX * sizeY * sizeZ;
            items = new T[sizeY][][];

            for (int y = 0; y < sizeY; y++)
            {
                var itemsZ = new T[sizeZ][];
                items[y] = itemsZ;

                for (int z = 0; z < sizeZ; z++)
                {
                    itemsZ[z] = new T[sizeX];
                }
            }
        }

        /// <summary>
        /// Returns value from an array
        /// </summary>
        public T Get(int x, int y, int z)
        {
            return items[y][z][x];
        }
        /// <summary>
        /// Returns value from an array
        /// </summary>
        public T Get(Vector3 position)
        {
            return items[(int)position.Y][(int)position.Z][(int)position.X];
        }

        /// <summary>
        /// Sets value to an array
        /// </summary>
        public void Set(int x, int y, int z, T value)
        {
            items[y][z][x] = value;
        }
        /// <summary>
        /// Sets value to an array
        /// </summary>
        public void Set(Vector3 position, T value)
        {
            items[(int)position.Y][(int)position.Z][(int)position.X] = value;
        }

        /// <summary>
        /// Returns this array items
        /// </summary>
        public T[][][] GetItems()
        {
            return items;
        }

        /// <summary>
        /// Returns fragment from an three-dimensional array
        /// </summary>
        public T[][][] GetFragment(int sizeX, int sizeY, int sizeZ)
        {
            var otherItems = new T[sizeY][][];

            for (int y = 0; y < sizeY; y++)
            {
                var itemsZ = items[y];
                var otherItemsZ = new T[sizeZ][];
                otherItems[y] = otherItemsZ;

                for (int z = 0; z < sizeZ; z++)
                {
                    var otherItemsX = new T[sizeX];
                    otherItemsZ[z] = otherItemsX;
                    Array.Copy(itemsZ[z], 0, otherItemsX, 0, sizeX);
                }
            }

            return otherItems;
        }
        /// <summary>
        /// Returns fragment from an three-dimensional array
        /// </summary>
        public T[][][] GetFragment(int sizeX, int sizeY, int sizeZ, int offsetX, int offsetY, int offsetZ)
        {
            var otherItems = new T[sizeY][][];

            for (int y = 0; y < sizeY; y++)
            {
                var itemsZ = items[y + offsetY];
                var otherItemsZ = new T[sizeZ][];
                otherItems[y] = otherItemsZ;

                for (int z = 0; z < sizeZ; z++)
                {
                    var otherItemsX = new T[sizeX];
                    otherItemsZ[z] = otherItemsX;
                    Array.Copy(itemsZ[z + offsetZ], offsetX, otherItemsX, 0, sizeX);
                }
            }

            return otherItems;
        }

        /// <summary>
        /// Returns true if array fits into this array
        /// </summary>
        public bool IsFits(IArray3<T> array)
        {
            return array.SizeX <= sizeX && array.SizeY <= sizeY && array.SizeZ <= sizeZ;
        }
        /// <summary>
        /// Returns true if array fits into this array
        /// </summary>
        public bool IsFits(IArray3<T> array, int offsetX, int offsetY, int offsetZ)
        {
            return offsetX >= 0 && array.SizeX + offsetX <= sizeX && offsetY >= 0 && array.SizeY + offsetY <= sizeY && offsetZ >= 0 && array.SizeZ + offsetZ <= sizeZ;
        }

        /// <summary>
        /// Copies this three-dimensional array to another
        /// </summary>
        public void CopyTo(IArray3<T> array)
        {
            var otherItems = array.GetItems();

            for (int y = 0; y < sizeY; y++)
            {
                var itemsZ = items[y];
                var otherItemsZ = otherItems[y];

                for (int z = 0; z < sizeZ; z++)
                    Array.Copy(itemsZ[z], 0, otherItemsZ[z], 0, sizeX);
            }
        }
        /// <summary>
        /// Copies this three-dimensional array to another
        /// </summary>
        public void CopyTo(IArray3<T> array, int offsetX, int offsetY, int offsetZ)
        {
            var otherItems = array.GetItems();

            for (int y = 0; y < sizeY; y++)
            {
                var itemsZ = items[y];
                var otherItemsZ = otherItems[y + offsetY];

                for (int z = 0; z < sizeZ; z++)
                    Array.Copy(itemsZ[z], 0, otherItemsZ[z + offsetZ], offsetX, sizeX);
            }
        }
    }
}
