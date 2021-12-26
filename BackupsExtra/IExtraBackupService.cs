using Backups;

namespace BackupsExtra
{
    public interface IExtraBackupService : IBackupService
    {
        void Save(string path);
        void Load(string path);
    }
}