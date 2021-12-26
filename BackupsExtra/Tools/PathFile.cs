using Backups.FileSystem;

namespace BackupsExtra
{
    public class PathFile
    {
        public PathFile(BackupFile backupFile, string path)
        {
            BackupFile = backupFile;
            Path = path;
        }

        public BackupFile BackupFile { get; }
        public string Path { get; }
    }
}