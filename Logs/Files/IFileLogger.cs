using InjectorGames.SharedLibrary.Times;

namespace InjectorGames.SharedLibrary.Logs.Files
{
    /// <summary>
    /// File logger interface
    /// </summary>
    public interface IFileLogger : ILogger
    {
        /// <summary>
        /// Logger clock
        /// </summary>
        IClock Clock { get; }
    }
}
