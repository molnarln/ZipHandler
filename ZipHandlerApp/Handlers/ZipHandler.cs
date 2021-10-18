using System.IO;
using System.IO.Compression;

namespace ZipHandlerApp.Handlers
{
    class ZipHandler : ICompressFileHandler
    {
        public void ExtractCompressedFile(string path)
        {
            if (path is null)
            {
                throw new System.ArgumentNullException(nameof(path));
            }

            UnzipZipFile(path);

        }
        private void UnzipZipFile(string path)
        {
            if (path is null)
            {
                throw new System.ArgumentNullException(nameof(path));
            }

            ZipFile.ExtractToDirectory(Path.GetFullPath(path), Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)));
        }
    }

}
