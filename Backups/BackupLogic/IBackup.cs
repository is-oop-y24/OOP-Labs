namespace Backups
{
    public interface IBackup
    {
        BackupJob CreateJob(string jobName);
    }
}