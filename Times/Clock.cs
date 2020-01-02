using System.Diagnostics;

namespace InjectorGames.SharedLibrary.Times
{
    /// <summary>
    /// Clock class
    /// </summary>
    public class Clock : Stopwatch, IClock
    {
        /// <summary>
        /// Returns the total elapsed time in milliseconds
        /// </summary>
        public long MS => ElapsedMilliseconds;
    }
}
