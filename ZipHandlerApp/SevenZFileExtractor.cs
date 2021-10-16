namespace ZipHandlerApp
{
    class SevenZFileExtractor : CompressedFileExtractor
    {
        public override ICompressFileHandler GetFileHandler()
        {
            return new SevenZHandler();
        }
    }
}
