# ZipHandler
A tool to recursively unzip 7z and zip files in a folder and delete already processed compressed files. 

This tool is using 7-Zip Extra standalone console version. The version that this current implementation is based on is 19.00. 
This version's binaries are included in this project, but also available at the official site of 7-zip: 

https://www.7-zip.org/download.html

To build the project, in Visual Studio add the 3 files (7zxa.dll, 7za.dll, 7za.exe) as existing items (as links), and in the file properties select the "Copy to Output Directory" for each file.
