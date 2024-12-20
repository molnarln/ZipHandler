﻿using System;
using System.Diagnostics;
using System.IO;

namespace ZipHandlerApp.Handlers
{
    class SevenZHandler : ICompressFileHandler
    {
        public void ExtractCompressedFile(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            UnzipSevenZFile(path);
        }

        private void UnzipSevenZFile(string path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string zPath = "7za.exe"; //add to proj and set CopyToOuputDir

            ProcessStartInfo pro = new ProcessStartInfo();
            pro.WindowStyle = ProcessWindowStyle.Hidden;
            pro.FileName = zPath;
            pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", Path.GetFullPath(path), Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path)));
            Process x = Process.Start(pro);
            x.WaitForExit();
        }
    }

}
