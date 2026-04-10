using System.IO;
using ZipHandlerApp.Handlers;
using ZipHandlerApp.Logging;

namespace ZipHandlerApp.Extractors
{
    abstract class CompressedFileExtractor
    {
        public abstract ICompressFileHandler GetFileHandler();

        public virtual void ExtractFile(string path, ILogger logger)
        {
            ICompressFileHandler handler = GetFileHandler();
            logger.Log($"The {Path.GetFileName(path)} file is going to be extracted...");
            handler.ExtractCompressedFile(path);
        }
    }
}
