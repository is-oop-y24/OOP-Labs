using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.FileSystem;

namespace Backups
{
    public class BackupService : IBackupService
    {
        private readonly List<IBackupJob> _jobs = new List<IBackupJob>();

        public BackupService(string path)
        {
            Path = path;
        }

        public ReadOnlyCollection<IBackupJob> Jobs => _jobs.AsReadOnly();
        public string Path { get; }

        public IBackupJob CreateJob(IJobBuilder jobBuilder)
        {
            IBackupJob backupJob = jobBuilder.GetJob();
            _jobs.Add(backupJob);
            return backupJob;
        }

        public IBackupJob FindJob(string jobName)
        {
            if (!_jobs.Exists(job => job.Name == jobName))
                throw new BackupException("Job doesnt exist.");
            return _jobs.Find(job => job.Name == jobName);
        }
    }
}