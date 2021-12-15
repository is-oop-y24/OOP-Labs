using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;

namespace BackupsExtra
{
    public class ExtraJobBuilder : IExtraJobBuilder
    {
        private string _destinationPath;
        private string _jobName;
        private IFileRepository _repository;
        private IStoragePacker _storagePacker;
        private IExcessPointsChooser _excessPointsChooser;
        private IJobCleaner _jobCleaner;
        private ILogger _logger;
        private IPointRestorer _pointRestorer;
        private ReadOnlyCollection<RestorePoint> _restorePoints;
        private ReadOnlyCollection<IJobObject> _jobObjects;

        public void SetDestinationPath(string destinationPath)
        {
            _destinationPath = destinationPath;
        }

        public void SetJobName(string jobName)
        {
            _jobName = jobName;
        }

        public void SetFileRepository(IFileRepository fileRepository)
        {
            _repository = fileRepository;
        }

        public void SetStoragePacker(IStoragePacker storagePacker)
        {
            _storagePacker = storagePacker;
        }

        public void SetExcessPointsChooser(IExcessPointsChooser chooser)
        {
            _excessPointsChooser = chooser;
        }

        public void SetJobCleaner(IJobCleaner jobCleaner)
        {
            _jobCleaner = jobCleaner;
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void SetPointsRestorer(IPointRestorer pointRestorer)
        {
            _pointRestorer = pointRestorer;
        }

        public void SetRestorePoints(ReadOnlyCollection<RestorePoint> restorePoints)
        {
            _restorePoints = restorePoints;
        }

        public void SetJobObjects(ReadOnlyCollection<IJobObject> jobObjects)
        {
            _jobObjects = jobObjects;
        }

        public ExtraBackupJob GetJob()
        {
            return new ExtraBackupJob(
                _destinationPath,
                _jobName,
                _repository,
                _storagePacker,
                _excessPointsChooser,
                _jobCleaner,
                _logger,
                _pointRestorer)
            {
                JobObjects = _jobObjects ?? new ReadOnlyCollection<IJobObject>(new List<IJobObject>()),
                RestorePoints = _restorePoints ?? new ReadOnlyCollection<RestorePoint>(new List<RestorePoint>()),
            };
        }
    }
}