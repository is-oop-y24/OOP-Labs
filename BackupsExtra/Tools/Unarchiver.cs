using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Backups;
using Backups.FileSystem;

namespace BackupsExtra
{
    public class Unarchiver : IUnarchiver
    {
        public List<PathFile> Unpack(BackupFile archive)
        {
            var files = new List<PathFile>();
            var decoder = new FileDecoder(archive);
            while (!decoder.FinishedReading)
            {
                long pathLength = decoder.GetNextLong();
                string filePath = decoder.GetNextString(pathLength);
                long fileLength = decoder.GetNextLong();
                byte[] content = decoder.GetNextByteContent(fileLength);

                string fileName = Path.GetFileName(filePath);
                files.Add(new PathFile(new BackupFile(new FileName(fileName), content), filePath));
            }

            return files;
        }
    }
}