namespace ZipHandlerApp
{
    class ZipFileExtractor : CompressedFileExtractor
    {
        public override ICompressFileHandler GetFileHandler()
        {
            return new ZipHandler();
        }
    }
}
