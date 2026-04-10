using System;
using System.IO;

namespace ZipHandlerApp.Logging
{
    internal sealed class ConsoleLogger : ILogger
    {
        private static readonly ConsoleLogger instance = new ConsoleLogger();

        private ConsoleLogger() { }

        static ConsoleLogger() { }
            
        public static ConsoleLogger Instance { get { return instance; } }

        public void Log(string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {message}";
            Console.WriteLine(logMessage);
        }
    }
}
