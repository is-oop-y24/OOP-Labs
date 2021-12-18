namespace Backups
{
    public interface IBackupService
    {
        IBackupJob CreateJob(IJobBuilder jobBuilder);
        IBackupJob FindJob(string jobName);
    }
}