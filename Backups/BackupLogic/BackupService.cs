using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups.FileSystem;

namespace Backups
{
    public class BackupService : IBackupService
    {
        private readonly List<BackupJob> _jobs = new List<BackupJob>();
        private readonly IFileRepository _repository;

        public BackupService(string path, IFileRepository repository)
        {
            Path = path;
            _repository = repository;
        }

        public ReadOnlyCollection<BackupJob> Jobs => _jobs.AsReadOnly();
        public IFileRepository FileRepository => _repository;
        public string Path { get; }

        public BackupJob CreateJob(string jobName, IStoragePacker storagePacker,  string jobPath = null)
        {
            if (_jobs.Exists(job => job.Name == jobName))
                throw new BackupException("Job already exists.");
            var job = new BackupJob(jobPath ?? Path, jobName, _repository, storagePacker);
            _jobs.Add(job);
            return job;
        }

        public BackupJob GetJob(string jobName)
        {
            if (!_jobs.Exists(job => job.Name == jobName))
                throw new BackupException("Job doesnt exist.");
            return _jobs.Find(job => job.Name == jobName);
        }
    }
}