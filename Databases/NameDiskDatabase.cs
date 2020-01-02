using InjectorGames.SharedLibrary.Collections.Bytes;
using InjectorGames.SharedLibrary.Entities;
using System.Collections.Generic;
using System.IO;

namespace InjectorGames.SharedLibrary.Databases
{
    /// <summary>
    /// Name disk database class
    /// </summary>
    public abstract class NameDiskDatabase<TName, TKey, TValue, TFactory> : DiskDatabase<TKey, TValue, TFactory>, INameDiskDatabase<TName, TKey, TValue, TFactory>
        where TValue : IByteArray, INameable<TName>, IIdentifiable<TKey>
        where TFactory : IByteArrayFactory<TValue>
    {
        /// <summary>
        /// Database name collection
        /// </summary>
        protected readonly Dictionary<TName, TKey> names;

        /// <summary>
        /// Creates a new name disk database class instance
        /// </summary>
        public NameDiskDatabase(string path, bool useCompression) : base(path, useCompression)
        {
            names = new Dictionary<TName, TKey>();
        }

        /// <summary>
        /// Returns true if the database contains key
        /// </summary>
        public bool ContainsKey(TName name)
        {
            lock (locker)
                return names.ContainsKey(name);
        }
        /// <summary>
        /// Removes account from the database
        /// </summary>
        public bool TryRemove(TName name)
        {
            lock (locker)
            {
                if (names.TryGetValue(name, out TKey key))
                    return TryRemove(key);
                else
                    return false;
            }
        }
        /// <summary>
        /// Removes account from the database
        /// </summary>
        public bool TryRemove(TName name, TFactory factory, out TValue value)
        {
            lock (locker)
            {
                if (names.TryGetValue(name, out TKey key))
                {
                    return TryRemove(key, factory, out value);
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }
        /// <summary>
        /// Returns account from the database
        /// </summary>
        public bool TryGetValue(TName name, TFactory factory, out TValue value)
        {
            lock (locker)
            {
                if (names.TryGetValue(name, out TKey key))
                {
                    return TryGetValue(key, factory, out value);
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }

        /// <summary>
        /// Loads database data
        /// </summary>
        public new void Load()
        {
            base.Load();
            LoadNames();
        }
        /// <summary>
        /// Unloads database data
        /// </summary>
        public new void Unload()
        {
            base.Unload();
            UnloadNames();
        }

        /// <summary>
        /// Adds a new item to the database
        /// </summary>
        public new bool TryAdd(TValue value)
        {
            var array = new byte[value.ByteArraySize];

            using (var memoryStream = new MemoryStream(array))
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    value.ToBytes(binaryWriter);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    lock (locker)
                    {
                        if (TryWriteStream(value, FileMode.CreateNew, memoryStream))
                        {
                            count++;
                            names.Add(value.Name, value.ID);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Adds new or updates existing item in the database
        /// </summary>
        public new bool AddOrUpdate(TValue value)
        {
            var array = new byte[value.ByteArraySize];

            using (var memoryStream = new MemoryStream(array))
            {
                using (var binaryWriter = new BinaryWriter(memoryStream))
                {
                    value.ToBytes(binaryWriter);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    lock (locker)
                    {
                        if (TryWriteStream(value, FileMode.CreateNew, memoryStream))
                        {
                            count++;
                            names.Add(value.Name, value.ID);
                            return true;
                        }
                        else
                        {
                            return TryWriteStream(value, FileMode.Create, memoryStream);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        public new bool TryRemove(TKey key)
        {
            try
            {
                lock (locker)
                {
                    File.Delete($"{path}{key}");
                    count--;

                    foreach (var name in names.Keys)
                    {
                        if (key.Equals(names[name]))
                        {
                            names.Remove(name);
                            break;
                        }
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Removes an existing item from the database
        /// </summary>
        public new bool TryRemove(TKey key, TFactory factory, out TValue value)
        {
            lock (locker)
            {
                if (TryGetValue(key, factory, out value))
                {
                    try
                    {
                        File.Delete($"{path}{key}");
                        count--;

                        foreach (var name in names.Keys)
                        {
                            if (key.Equals(names[name]))
                            {
                                names.Remove(name);
                                break;
                            }
                        }

                        return true;
                    }
                    catch
                    {
                        value = default;
                        return false;
                    }
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }

        /// <summary>
        /// Loads concurrent database names
        /// </summary>
        public abstract void LoadNames();
        /// <summary>
        /// Unloads concurrent database names
        /// </summary>
        public abstract void UnloadNames();
    }
}
