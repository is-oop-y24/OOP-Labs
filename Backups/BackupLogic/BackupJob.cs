using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class BackupJob : IBackupJob
    {
        private readonly List<IJobObject> _jobObjects = new List<IJobObject>();
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private readonly IFileRepository _fileRepository;
        private readonly IStoragePacker _storagePacker;

        public BackupJob(string jobName, string destinationPath, IFileRepository fileRepository, IStoragePacker storagePacker)
        {
            if (jobName == null)
                throw new NullReferenceException(nameof(jobName));
            if (destinationPath == null)
                throw new NullReferenceException(nameof(destinationPath));

            Path = System.IO.Path.Combine(destinationPath, jobName);
            Name = jobName;
            _fileRepository = fileRepository ?? throw new NullReferenceException(nameof(fileRepository));
            _storagePacker = storagePacker ?? throw new NullReferenceException(nameof(storagePacker));
        }

        public ReadOnlyCollection<IJobObject> JobObjects => _jobObjects.AsReadOnly();
        public ReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints.AsReadOnly();
        public IFileRepository FileRepository => _fileRepository;
        public IStoragePacker StoragePacker => _storagePacker;
        public string Path { get; }

        public string Name { get; }

        public void AddObject(IJobObject jobObject)
        {
            if (jobObject == null)
                throw new NullReferenceException(nameof(jobObject));
            _jobObjects.Add(jobObject);
        }

        public void DeleteObject(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BackupException("Incorrect object name");
            if (_jobObjects.RemoveAll(job => job.Name == name) == 0)
                throw new BackupException("File doesnt exist.");
        }

        public RestorePoint GetRestorePoint(int id)
        {
            return _restorePoints
                       .Find(rp => rp.Id == id)
                   ?? throw new BackupException("Job doesn't contain this restore point.");
        }

        public RestorePoint MakeRestorePoint()
        {
            var restorePoint = new RestorePoint(Path, _jobObjects, _storagePacker);
            restorePoint.Process(_fileRepository);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }
    }
}