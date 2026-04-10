using System;
using System.IO;
using ZipHandlerApp.Enums;
using ZipHandlerApp.Extractors;
using ZipHandlerApp.Logging;

namespace ZipHandlerApp
{
    class Program
    {
        static CompressedFileExtractor SevenZFileExtractor;
        static CompressedFileExtractor ZipExtractor;
        static LogType logType;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Missing arguments: <path> <c/f>");
                return;
            }
            string[] paths = { Path.GetDirectoryName(args[0]) };

            string logTypeString = args[1];

            switch (logTypeString)
            {
                case "c":
                    logType = LogType.CONSOLE;
                    break;
                case "f":
                    logType = LogType.FILE;
                    break;
                default:
                    logType = LogType.CONSOLE;
                    break;
            }

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
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
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
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".zip":
                        ZipExtractor ??= new ZipFileExtractor();
                        ExtractAndDeleteFile(ZipExtractor, path);
                        break;

                    case ".7z":
                        SevenZFileExtractor ??= new SevenZFileExtractor();
                        ExtractAndDeleteFile(SevenZFileExtractor, path);
                        break;

                }

                Console.WriteLine("Processed file '{0}'.", path);
            }

            static void ExtractAndDeleteFile(CompressedFileExtractor extractor, string path)
            {
                switch (logType)
                {
                    case LogType.CONSOLE:
                        extractor.ExtractFile(path, ConsoleLogger.Instance);
                        break;
                    case LogType.FILE:
                        extractor.ExtractFile(path, FileLogger.Instance);
                        break;
                    default:
                        extractor.ExtractFile(path, ConsoleLogger.Instance);
                        break;
                }
                File.Delete(path);
            }
        }
    }
}
