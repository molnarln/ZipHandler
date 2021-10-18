using ZipHandlerApp.Handlers;

namespace ZipHandlerApp.Extractors
{
    class SevenZFileExtractor : CompressedFileExtractor
    {
        public override ICompressFileHandler GetFileHandler()
        {
            return new SevenZHandler();
        }
    }
}
