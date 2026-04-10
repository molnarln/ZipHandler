using System;
using System.Collections.Generic;
using System.IO;
using ZipHandlerApp.Extractors;
using ZipHandlerApp.Logging;

namespace ZipHandlerApp.Strategies
{
    internal class ExtractorContext
    {
        private readonly Dictionary<string, CompressedFileExtractor> _strategies = new Dictionary<string, CompressedFileExtractor>();

        public ExtractorContext()
        {
            _strategies.Add(".zip", new ZipFileExtractor());
            _strategies.Add(".7z", new SevenZFileExtractor());
        }

        public void ExecuteStrategy(string path, ILogger logger)
        {
            string extension = Path.GetExtension(path).ToLower();

            if (_strategies.ContainsKey(extension))
            {
                var extractor = _strategies[extension];
                extractor.ExtractFile(path, logger);
                File.Delete(path);
            }
            else
            {
                Console.WriteLine($"No extraction strategy found for: {extension}");
            }
        }
    }
}
