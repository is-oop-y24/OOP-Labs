using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;
using Kfc.Utility.Extensions;

namespace BackupsExtra
{
    public class ExtraBackupJob : BackupJob
    {
        private readonly IExcessPointsChooser _excessPointsChooser;
        private readonly IJobCleaner _jobCleaner;
        private readonly IJobSaver _jobSaver;
        private readonly ILogger _logger;
        private readonly IPointRestorer _pointRestorer;

        public ExtraBackupJob(string destinationPath, string jobName, IFileRepository fileRepository, IStoragePacker storagePacker, IExcessPointsChooser excessPointsChooser, IJobCleaner jobCleaner, IJobSaver jobSaver, ILogger logger, IPointRestorer pointRestorer)
            : base(destinationPath, jobName, fileRepository, storagePacker)
        {
            _excessPointsChooser = excessPointsChooser.ThrowIfNull(nameof(excessPointsChooser));
            _jobCleaner = jobCleaner.ThrowIfNull(nameof(jobCleaner));
            _jobSaver = jobSaver.ThrowIfNull(nameof(jobSaver));
            _logger = logger.ThrowIfNull(nameof(logger));
            _pointRestorer = pointRestorer.ThrowIfNull(nameof(pointRestorer));
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void CleanPoints()
        {
            throw new System.NotImplementedException();
        }

        public void RestorePoint()
        {
            throw new System.NotImplementedException();
        }
    }
}