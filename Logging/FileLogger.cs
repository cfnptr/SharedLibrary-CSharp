
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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace OpenSharedLibrary.Logging
{
    /// <summary>
    /// File logger class
    /// </summary>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// Stopwatch timer
        /// </summary>
        protected readonly Stopwatch timer;
        /// <summary>
        /// Logger file stream
        /// </summary>
        protected readonly FileStream stream;

        /// <summary>
        /// Logger logging level
        /// </summary>
        public LogType Level { get; set; }

        /// <summary>
        /// Creates a new file stream logger class instance
        /// </summary>
        public FileLogger(Stopwatch timer, LogType level, string logFolderPath)
        {
            this.timer = timer ?? throw new ArgumentNullException();

            Level = level;

            if (!Directory.Exists(logFolderPath))
                Directory.CreateDirectory(logFolderPath);

            var filePath = logFolderPath + DateTime.Now.ToString("yyyy-M-dd_HH-mm-ss");
            stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);

            Info($"File logger started (dateTime: {DateTime.Now.ToLongTimeString()}, milliseconds: {timer.ElapsedMilliseconds})");
        }

        /// <summary>
        /// Returns true if logger should log this level
        /// </summary>
        public bool Log(LogType level) { return level <= Level; }

        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public void Fatal(object message)
        {
            var logMessage = $"[{timer.ElapsedMilliseconds}] [{Thread.CurrentThread.ManagedThreadId}] [Fatal]: {message}\n";
            WriteToStream(logMessage);
            OnFatalLog(logMessage);
        }
        /// <summary>
        /// Logs a new message at error log level
        /// </summary>
        public void Error(object message) { WriteToStream($"[{timer.ElapsedMilliseconds}] [{Thread.CurrentThread.ManagedThreadId}] [Error]: {message}\n"); }
        /// <summary>
        /// Logs a new message at info log level
        /// </summary>
        public void Info(object message) { WriteToStream($"[{timer.ElapsedMilliseconds}] [{Thread.CurrentThread.ManagedThreadId}] [Info]: {message}\n"); }
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public void Debug(object message) { WriteToStream($"[{timer.ElapsedMilliseconds}] [{Thread.CurrentThread.ManagedThreadId}] [Debug]: {message}\n"); }
        /// <summary>
        /// Logs a new message at fatal log level
        /// </summary>
        public void Trace(object message) { WriteToStream($"[{timer.ElapsedMilliseconds}] [{Thread.CurrentThread.ManagedThreadId}] [Trace]: {message}\n"); }

        /// <summary>
        /// Closes logger stream
        /// </summary>
        public void Close()
        {
            lock (stream)
                stream.Close();
        }

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
            }
        }

        /// <summary>
        /// On fatal log message event
        /// </summary>
        protected virtual void OnFatalLog(string message) { }
    }
}
