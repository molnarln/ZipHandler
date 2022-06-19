using System;
using System.IO;
using System.Runtime.CompilerServices;
using ZipHandlerApp.Extractors;

namespace ZipHandlerApp
{
    class Program
    {
        static CompressedFileExtractor SevenZFileExtractor;
        static CompressedFileExtractor ZipExtractor;

        static void Main(string[] args)
        {
            string[] paths = { Path.GetDirectoryName(args[0]) };

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
                extractor.ExtractFile(path);
                File.Delete(path);
            }
        }
    }
}
