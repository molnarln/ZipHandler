using System;
using System.IO;

namespace ZipHandlerApp.Logging
{
    internal sealed class FileLogger : ILogger
    {
        private static readonly FileLogger instance = new FileLogger();

        private readonly string _logPath = "ZipHandler_log.txt";

        private static readonly object _lock = new object();

        private FileLogger() { }

        static FileLogger()
        {
            Instance.Log("--- Logging initialized ---");
        }

        public static FileLogger Instance => instance;

        public void Log(string message)
        {
            try
            {
                lock (_lock)
                {
                    string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
                    File.AppendAllText(_logPath, logMessage + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log failed: {ex.Message}");
            }
        }
    }
}
