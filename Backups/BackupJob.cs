using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class BackupJob
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private readonly string _jobPath;

        public BackupJob(string jobPath)
        {
            _jobPath = jobPath;
        }
        
        public StorageMode StorageMode { get; set; } = StorageMode.SingleStorage;

        public void AddFile(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void MakeRestorePoint()
        {
            var restorePoint = new RestorePoint(_jobPath);
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