using System.Collections.Generic;
using System.Collections.ObjectModel;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;
using Kfc.Utility.Extensions;

namespace BackupsExtra
{
    public class ExtraBackupJob : IBackupJob
    {
        private readonly BackupJob _backupJob;
        private readonly IExcessPointsChooser _excessPointsChooser;
        private readonly IJobCleaner _jobCleaner;
        private readonly ILogger _logger;
        private readonly IPointRestorer _pointRestorer;

        private List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public ExtraBackupJob(string destinationPath, string jobName, IFileRepository fileRepository, IStoragePacker storagePacker, IExcessPointsChooser excessPointsChooser, IJobCleaner jobCleaner, ILogger logger, IPointRestorer pointRestorer)
        {
            _excessPointsChooser = excessPointsChooser.ThrowIfNull(nameof(excessPointsChooser));
            _jobCleaner = jobCleaner.ThrowIfNull(nameof(jobCleaner));
            _logger = logger.ThrowIfNull(nameof(logger));
            _pointRestorer = pointRestorer.ThrowIfNull(nameof(pointRestorer));
            _backupJob = new BackupJob(destinationPath, jobName, fileRepository, storagePacker);
            _logger.Log($"Job {jobName} created.");
        }

        public IJobCleaner JobCleaner => _jobCleaner;
        public ILogger Logger => _logger;
        public IPointRestorer PointRestorer => _pointRestorer;
        public IFileRepository FileRepository => _backupJob.FileRepository;
        public IStoragePacker StoragePacker => _backupJob.StoragePacker;
        public IExcessPointsChooser ExcessPointsChooser => _excessPointsChooser;

        public string Path => _backupJob.Path;
        public string Name => _backupJob.Name;

        public ReadOnlyCollection<IJobObject> JobObjects
        {
            get => _backupJob.JobObjects;
            init
            {
                foreach (IJobObject jobObject in value)
                    _backupJob.AddObject(jobObject);
            }
        }

        public ReadOnlyCollection<RestorePoint> RestorePoints
        {
            get => _restorePoints.AsReadOnly();
            init => _restorePoints = new List<RestorePoint>();
        }

        public void AddObject(IJobObject jobObject)
        {
            _backupJob.AddObject(jobObject);
            _logger.Log($"Object {jobObject.Name} is added to the job.");
        }

        public void DeleteObject(string name)
        {
            _backupJob.DeleteObject(name);
        }

        public RestorePoint MakeRestorePoint()
        {
            _logger.Log("Restore point processing begins.");
            RestorePoint restorePoint = _backupJob.MakeRestorePoint();
            _restorePoints.Add(restorePoint);
            _logger.Log("Restore point created.");
            CleanPoints();
            return restorePoint;
        }

        public void RestoreThePoint(RestorePoint restorePoint)
        {
            _pointRestorer.RestoreThePoint(restorePoint);
        }

        private void CleanPoints()
        {
            _logger.Log("Cleaning of points begins");
            _restorePoints = _jobCleaner.GetCleanedList(
                _restorePoints,
                _excessPointsChooser.ChoosePoints(_restorePoints));
            _logger.Log("Points cleaning finished.");
        }
    }
}