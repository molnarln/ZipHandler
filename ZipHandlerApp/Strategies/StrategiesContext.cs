using System;
using System.Collections.Generic;
using System.IO;
using ZipHandlerApp.Extractors;
using ZipHandlerApp.Logging;

namespace ZipHandlerApp.Strategies
{
    internal class ExtractorContext
    {
        private readonly Dictionary<string, CompressedFileExtractor> _strategies =
            new Dictionary<string, CompressedFileExtractor>
        {
            { ".zip", new ZipFileExtractor() },
            { ".7z", new SevenZFileExtractor() }
        };

        private CompressedFileExtractor _currentStrategy;

        public bool TrySetStrategy(string extension)
        {
            if (_strategies.TryGetValue(extension, out var strategy))
            {
                _currentStrategy = strategy;
                return true;
            }
            return false;
        }

        public void Execute(string path, ILogger logger)
        {
            _currentStrategy?.ExtractFile(path, logger);
            File.Delete(path);
        }
    }
}
