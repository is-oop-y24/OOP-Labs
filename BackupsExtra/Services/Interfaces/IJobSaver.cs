using Backups;

namespace BackupsExtra.Services.Services
{
    public interface IJobSaver
    {
        void Save(ExtraBackupJob backupJob, string path);
        ExtraBackupJob Load(string jobSettingsPath);
    }
}