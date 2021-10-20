using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private readonly IFileRepository _fileRepository;
        private readonly string _fullPath;

        public BackupJob(string path, string jobName, IFileRepository fileRepository)
        {
            _fullPath = Path.Combine(path, jobName);
            Name = jobName;
            _fileRepository = fileRepository;
        }

        public StorageMode StorageMode { get; set; } = StorageMode.SingleStorage;
        public string Name { get; }

        public void AddObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void DeleteObject(string jobName)
        {
            if (_jobObjects.RemoveAll(job => job.Name == jobName) == 0)
                throw new BackupException("File doesnt exist.");
        }

        public void MakeRestorePoint()
        {
            var date = DateTime.Now;
            var restorePoint = new RestorePoint(Path.Combine(_fullPath, $"{date.Day}.{date.Month}.{date.Year} [Time {date.Hour}.{date.Minute}.{date.Second}]"), _fileRepository);
            switch (StorageMode)
            {
                case StorageMode.SingleStorage:
                    restorePoint.AddStorage(_jobObjects);
                    break;
                case StorageMode.SplitStorage:
                    _jobObjects.ForEach(jobObject => restorePoint.AddStorage(jobObject));
                    break;
                default:
                    throw new BackupException("Incorrect storage mode.");
            }

            _restorePoints.Add(restorePoint);
        }
    }
}