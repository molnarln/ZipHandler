using System;
using System.Collections.Generic;
using System.IO;
using ZipHandlerApp.Extractors;
using ZipHandlerApp.Logging;

namespace ZipHandlerApp.Strategies
{
    internal class ExtractorContext
    {
        private CompressedFileExtractor _strategy;

        public void SetStrategy(CompressedFileExtractor strategy)
        {
            this._strategy = strategy;
        }

        public void Execute(string path, ILogger logger)
        {
            if (_strategy != null)
            {
                _strategy.ExtractFile(path, logger);
            }
        }
    }
}
