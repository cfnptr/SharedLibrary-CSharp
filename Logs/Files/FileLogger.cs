using InjectorGames.SharedLibrary.Times;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace InjectorGames.SharedLibrary.Logs.Files
{
    /// <summary>
    /// File logger class
    /// </summary>
    public class FileLogger : Logger, IFileLogger
    {
        /// <summary>
        /// Logger clock
        /// </summary>
        protected readonly IClock clock;
        /// <summary>
        /// Logger file stream
        /// </summary>
        protected readonly FileStream stream;

        /// <summary>
        /// Logger clock
        /// </summary>
        public IClock Clock => clock;

        /// <summary>
        /// Creates a new file stream logger class instance
        /// </summary>
        public FileLogger(IClock clock, LogType level, bool writeToConsole, string logFolderPath)
        {
            this.clock = clock ?? throw new ArgumentNullException();

            Level = level;
            WriteToConsole = writeToConsole;

            if (!Directory.Exists(logFolderPath))
                Directory.CreateDirectory(logFolderPath);

            var filePath = logFolderPath + DateTime.Now.ToString("yyyy-M-dd_HH-mm-ss");
            stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        }

        /// <summary>
        /// Closes file logger
        /// </summary>
        public override void Close()
        {
            lock (stream)
                stream.Close();
        }

        /// <summary>
        /// Returns true if logger should log this level
        /// </summary>
        public override bool Log(LogType level) { return level <= Level; }

        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public override void Fatal(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Fatal]: {message}\n"); }
        /// <summary>
        /// Logs a new message at error log level
        /// </summary>
        public override void Error(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Error]: {message}\n"); }
        /// <summary>
        /// Logs a new message at warning log level
        /// </summary>
        public override void Warning(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Warning]: {message}\n"); }
        /// <summary>
        /// Logs a new message at info log level
        /// </summary>
        public override void Info(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Info]: {message}\n"); }
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public override void Debug(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Debug]: {message}\n"); }
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public override void Trace(object message) { WriteToStream($"[{clock.MS}] [{Thread.CurrentThread.ManagedThreadId}] [Trace]: {message}\n"); }

        /// <summary>
        /// Writes logger message to the file stream
        /// </summary>
        protected void WriteToStream(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            lock (stream)
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();

                if (WriteToConsole)
                    Console.Write(message);
            }
        }
    }
}
