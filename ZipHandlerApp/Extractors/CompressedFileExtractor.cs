using System;
using System.IO;
using ZipHandlerApp.Handlers;

namespace ZipHandlerApp.Extractors
{
    abstract class CompressedFileExtractor
    {
        public abstract ICompressFileHandler GetFileHandler();

        public virtual void ExtractFile(string path)
        {
            ICompressFileHandler handler = GetFileHandler();
            Console.WriteLine($"The {Path.GetFileName(path)} file is going to be extrated...");
            handler.ExtractCompressedFile(path);
        }
    }
}
