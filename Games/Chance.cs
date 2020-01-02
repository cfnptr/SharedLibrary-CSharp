using System;

namespace InjectorGames.SharedLibrary.Games
{
    /// <summary>
    /// Chance container structure (from 0.0f to 100.0f)
    /// </summary>
    public struct Chance
    {
        /// <summary>
        /// Chance value
        /// </summary>
        private readonly float value;

        /// <summary>
        /// Creates a new chance structure instance
        /// </summary>
        public Chance(float value)
        {
            if (value < 0.0f || value > 100.0f)
                throw new ArgumentOutOfRangeException(nameof(value));

            this.value = value;
        }

        /// <summary>
        /// Returns chance value
        /// </summary>
        public static implicit operator float(Chance chance)
        {
            return chance.value;
        }
        /// <summary>
        /// Returns chance value
        /// </summary>
        public static implicit operator Chance(float value)
        {
            return new Chance(value);
        }
    }
}
