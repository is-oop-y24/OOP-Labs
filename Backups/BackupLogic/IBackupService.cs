namespace Backups
{
    public interface IBackupService
    {
        BackupJob CreateJob(string jobName, IStoragePacker storagePacker, string jobPath = null);
        BackupJob GetJob(string jobName);
    }
}