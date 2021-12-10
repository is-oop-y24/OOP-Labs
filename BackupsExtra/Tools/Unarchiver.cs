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
        public List<BackupFile> Unpack(BackupFile archive)
        {
            var files = new List<BackupFile>();
            var decoder = new FileDecoder(archive);
            while (!decoder.FinishedReading)
            {
                long nameLength = decoder.GetNextLong();
                string fileName = decoder.GetNextString(nameLength);
                long fileLength = decoder.GetNextLong();
                byte[] content = decoder.GetNextByteContent(fileLength);
                files.Add(new BackupFile(new FileName(fileName), content));
            }

            return files;
        }
    }
}