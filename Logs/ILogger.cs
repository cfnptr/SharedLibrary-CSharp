namespace InjectorGames.SharedLibrary.Logs
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logger logging level
        /// </summary>
        LogType Level { get; set; }
        /// <summary>
        /// Write log messages to the console
        /// </summary>
        bool WriteToConsole { get; set; }

        /// <summary>
        /// Closes logger
        /// </summary>
        void Close();

        /// <summary>
        /// Returns true if logger should log this level
        /// </summary>
        bool Log(LogType level);

        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        void Fatal(object message);
        /// <summary>
        /// Logs a new message at error log level
        /// </summary>
        void Error(object message);
        /// <summary>
        /// Logs a new message at warning log level
        /// </summary>
        void Warning(object message);
        /// <summary>
        /// Logs a new message at info log level
        /// </summary>
        void Info(object message);
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        void Debug(object message);
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        void Trace(object message);
    }
}
