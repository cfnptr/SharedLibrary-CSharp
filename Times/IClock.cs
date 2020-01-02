namespace InjectorGames.SharedLibrary.Times
{
    /// <summary>
    /// Clock interface
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Returns the total elapsed time in milliseconds
        /// </summary>
        long MS { get; }
    }
}
