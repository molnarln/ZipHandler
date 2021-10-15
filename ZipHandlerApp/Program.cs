using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace ZipHandlerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] paths = { @"c:\Users\laszl\SourceCodes\ZipHandlerApp\testfolder\" };

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
                string[] fileEntries = System.IO.Directory.GetFiles(targetDirectory);
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
                if (Path.GetExtension(path).Equals(".zip", StringComparison.OrdinalIgnoreCase)) UnzipZipFile(path);

                if (Path.GetExtension(path).Equals(".7z", StringComparison.OrdinalIgnoreCase)) Unzip7zFile(path);

                File.Delete(path);

                Console.WriteLine("Processed file '{0}'.", path);
            }

            static void UnzipZipFile(string path)
            {
                ZipFile.ExtractToDirectory(Path.GetFullPath(path), Path.GetDirectoryName(path));
            }

            static void Unzip7zFile(string path)
            {
                string zPath = "7za.exe"; //add to proj and set CopyToOuputDir

                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = zPath;
                pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", Path.GetFullPath(path), Path.GetDirectoryName(path));
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
        }
    }
}
