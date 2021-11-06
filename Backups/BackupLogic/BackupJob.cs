using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class BackupJob
    {
        private readonly List<IJobObject> _jobObjects = new List<IJobObject>();
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private readonly IFileRepository _fileRepository;
        private readonly IStoragePacker _storagePacker;
        private readonly string _path;

        public BackupJob(string destinationPath, string jobName, IFileRepository fileRepository, IStoragePacker storagePacker)
        {
            _path = Path.Combine(destinationPath, jobName);
            Name = jobName;
            _fileRepository = fileRepository;
            _storagePacker = storagePacker;
        }
        public string Name { get; }

        public void AddObject(IJobObject jobObject)
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
            _restorePoints.Add(new RestorePoint(_path, _jobObjects, _storagePacker, _fileRepository));
        }
    }
}