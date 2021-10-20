using System.Collections.Generic;
using Backups.FileSystem;

namespace Backups
{
    public class Backup : IBackup
    {
        private readonly List<BackupJob> _jobs = new List<BackupJob>();
        private readonly string _path;
        private readonly IFileRepository _repository;

        public Backup(string path, IFileRepository repository)
        {
            _path = path;
            _repository = repository;
        }

        public BackupJob CreateJob(string jobName, StorageMode storageMode)
        {
            if (_jobs.Exists(job => job.Name == jobName))
                throw new BackupException("Job already exists.");
            var job = new BackupJob(_path, jobName, _repository) { StorageMode = storageMode };
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