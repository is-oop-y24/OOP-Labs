using Backups.FileSystem;

namespace Backups
{
    public interface IArchiver
    {
        void AddFile(File file);
        File MakeArchive(FileName archiveName);
    }
}