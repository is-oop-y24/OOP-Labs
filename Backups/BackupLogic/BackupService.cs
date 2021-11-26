using System.Collections.Generic;
using Backups.FileSystem;

namespace Backups
{
    public class BackupService : IBackupService
    {
        private readonly List<BackupJob> _jobs = new List<BackupJob>();
        private readonly string _path;
        private readonly IFileRepository _repository;

        public BackupService(string path, IFileRepository repository)
        {
            _path = path;
            _repository = repository;
        }

        public BackupJob CreateJob(string jobName, IStoragePacker storagePacker,  string jobPath = null)
        {
            if (_jobs.Exists(job => job.Name == jobName))
                throw new BackupException("Job already exists.");
            var job = new BackupJob(jobPath ?? _path, jobName, _repository, storagePacker);
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