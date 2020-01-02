namespace InjectorGames.SharedLibrary.Logs
{
    /// <summary>
    /// Abstract logger class
    /// </summary>
    public abstract class Logger : ILogger
    {
        /// <summary>
        /// Logger logging level
        /// </summary>
        public LogType Level { get; set; }
        /// <summary>
        /// Write log messages to the console
        /// </summary>
        public bool WriteToConsole { get; set; }

        /// <summary>
        /// Closes logger
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Returns true if logger should log this level
        /// </summary>
        public abstract bool Log(LogType level);

        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public abstract void Fatal(object message);
        /// <summary>
        /// Logs a new message at error log level
        /// </summary>
        public abstract void Error(object message);
        /// <summary>
        /// Logs a new message at warning log level
        /// </summary>
        public abstract void Warning(object message);
        /// <summary>
        /// Logs a new message at info log level
        /// </summary>
        public abstract void Info(object message);
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public abstract void Debug(object message);
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public abstract void Trace(object message);
    }
}
