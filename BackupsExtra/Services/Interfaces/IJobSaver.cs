using Backups;

namespace BackupsExtra.Services.Services
{
    public interface IJobSaver
    {
        void Save(IBackupJob backupJob, string path);
        IBackupJob Load(string jobSettingsPath);
    }
}