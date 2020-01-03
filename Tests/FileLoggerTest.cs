using InjectorGames.SharedLibrary.Logs;
using InjectorGames.SharedLibrary.Logs.Files;
using InjectorGames.SharedLibrary.Times;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace InjectorGames.SharedLibrary.Tests
{
    [TestClass]
    public class FileLoggerTest
    {
        public const string path = "unit-tests-logs/";

        [TestMethod]
        public void TestLogging()
        {
            var clock = new Clock();
            clock.Start();

            var logger = new FileLogger(clock, LogType.All, false, path) as ILogger;

            if (logger.Log(LogType.Trace))
                logger.Trace("Test Trace");
            if (logger.Log(LogType.Debug))
                logger.Debug("Test Debug");
            if (logger.Log(LogType.Info))
                logger.Info("Test Info");
            if (logger.Log(LogType.Warning))
                logger.Warning("Test Warning");
            if (logger.Log(LogType.Error))
                logger.Error("Test Error");
            if (logger.Log(LogType.Fatal))
                logger.Error("Test Fatal");

            logger.Level = LogType.Info;
            if (logger.Log(LogType.Info))
                logger.Info("Test Info Equality");

            logger.Level = LogType.Off;
            if (logger.Log(LogType.Error))
                logger.Error("Test Off");

            logger.Close();

            var files = Directory.GetFiles(path);
            var lines = File.ReadAllLines(files[0]);

            if (lines.Length != 7)
                throw new Exception("Wrong log line count");

            if (!lines[0].Contains("Trace") || !lines[0].Contains("Test Trace"))
                throw new Exception("Not full trace log message");
            if (!lines[1].Contains("Debug") || !lines[1].Contains("Test Debug"))
                throw new Exception("Not full debug log message");
            if (!lines[2].Contains("Info") || !lines[2].Contains("Test Info"))
                throw new Exception("Not full info log message");
            if (!lines[3].Contains("Warning") || !lines[3].Contains("Test Warning"))
                throw new Exception("Not full warning log message");
            if (!lines[4].Contains("Error") || !lines[4].Contains("Test Error"))
                throw new Exception("Not full error log message");
            if (!lines[5].Contains("Fatal") || !lines[5].Contains("Test Fatal"))
                throw new Exception("Not full fatal log message");
            if (!lines[6].Contains("Info") || !lines[6].Contains("Test Info Equality"))
                throw new Exception("Not full info equality log message");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
    }
}
