namespace Backups
{
    public interface IBackup
    {
        BackupJob CreateJob(string jobName, StorageMode storageMode);
        BackupJob GetJob(string jobName);
    }
}