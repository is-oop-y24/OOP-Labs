using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;
using BackupsExtra;

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
        private IJobSaver _jobSaver;
        private ILogger _logger;
        private IPointRestorer _pointRestorer;

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

        public void SetJobSaver(IJobSaver jobSaver)
        {
            _jobSaver = jobSaver;
        }

        public void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void SetPointsRestorer(IPointRestorer pointRestorer)
        {
            _pointRestorer = pointRestorer;
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
                _jobSaver,
                _logger,
                _pointRestorer);
        }
    }
}