using System;
using System.IO;
using ZipHandlerApp.Extractors;
using ZipHandlerApp.Logging;
using ZipHandlerApp.Strategies;

namespace ZipHandlerApp
{
    class Program
    {
        static ILogger _currentLogger;
        static ExtractorContext _context = new ExtractorContext();

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                _currentLogger.Log("Error: Missing arguments: <path> <c/f>");
                return;
            }
            string[] paths = { Path.GetDirectoryName(args[0]) };

            _currentLogger = args[1] switch
            {
                "f" => FileLogger.Instance,
                "c" => ConsoleLogger.Instance,
                _ => ConsoleLogger.Instance
            };

            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path);
                }
                else
                {
                    _currentLogger.Log($"{path} is not a valid file or directory.");
                }

                Console.WriteLine("Press any button to exit.");
                Console.ReadKey();
            }

            // Process all files in the directory passed in, recurse on any directories
            // that are found, and process the files they contain.
            static void ProcessDirectory(string targetDirectory)
            {
                // Process the list of files found in the directory.
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                    ProcessFile(fileName);

                // Recurse into subdirectories of this directory.
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory);
            }

            // Insert logic for processing found files here.
            static void ProcessFile(string path)
            {
                string extension = Path.GetExtension(path).ToLower();

                if (_context.TrySetStrategy(extension))
                {
                    _context.Execute(path, _currentLogger);
                    _currentLogger.Log($"Processed file {path}.");
                }
                else
                {
                    _currentLogger.Log($"Unknown extension: {extension}");
                }
            }
        }
    }
}
