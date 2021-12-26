using Backups.FileSystem;

namespace Backups
{
    public class JobBuilder : IJobBuilder
    {
        private string _destinationPath;
        private string _jobName;
        private IFileRepository _fileRepository;
        private IStoragePacker _storagePacker;

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
            _fileRepository = fileRepository;
        }

        public void SetStoragePacker(IStoragePacker storagePacker)
        {
            _storagePacker = storagePacker;
        }

        public IBackupJob GetJob()
        {
            return new BackupJob(_destinationPath, _jobName, _fileRepository, _storagePacker);
        }
    }
}