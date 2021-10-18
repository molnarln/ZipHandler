using ZipHandlerApp.Handlers;

namespace ZipHandlerApp.Extractors
{
    class ZipFileExtractor : CompressedFileExtractor
    {
        public override ICompressFileHandler GetFileHandler()
        {
            return new ZipHandler();
        }
    }
}
