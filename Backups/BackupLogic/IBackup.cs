namespace Backups
{
    public interface IBackup
    {
        BackupJob CreateJob(string jobName, StorageMode storageMode, string jobPath = null);
        BackupJob GetJob(string jobName);
    }
}