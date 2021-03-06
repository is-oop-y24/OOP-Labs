using System.Collections.Generic;
using Backups.FileSystem;

namespace BackupsExtra
{
    public interface IUnarchiver
    {
        List<PathFile> Unpack(BackupFile archive);
    }
}