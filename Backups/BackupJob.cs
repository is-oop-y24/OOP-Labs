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
        private readonly string _jobPath;

        public BackupJob(string jobPath, IFileRepository fileRepository)
        {
            _jobPath = jobPath;
            _fileRepository = fileRepository;
        }

        public StorageMode StorageMode { get; set; } = StorageMode.SingleStorage;

        public void AddFile(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void MakeRestorePoint()
        {
            var date = DateTime.Now;
            var restorePoint = new RestorePoint(Path.Combine(_jobPath, $"{date.Day}.{date.Month}.{date.Year} [Time {date.Hour}.{date.Minute}]"), _fileRepository);
            switch (StorageMode)
            {
                case StorageMode.SingleStorage:
                    restorePoint.AddStorage(_jobObjects);
                    break;
                case StorageMode.SplitStorage:
                    _jobObjects.ForEach(jobObject => restorePoint.AddStorage(jobObject));
                    break;
            }

            _restorePoints.Add(restorePoint);
        }
    }
}