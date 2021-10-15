using System.Text;

namespace Backups.FileSystem
{
    public interface IFileRepository
    {
        void AddFile(File file, string path);
        File GetFile(string filePath);
    }
}