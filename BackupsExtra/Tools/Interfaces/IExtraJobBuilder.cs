using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;
using BackupsExtra;

namespace BackupsExtra
{
    public interface IExtraJobBuilder
    {
        void SetDestinationPath(string destinationPath);
        void SetJobName(string jobName);
        void SetFileRepository(IFileRepository fileRepository);
        void SetStoragePacker(IStoragePacker storagePacker);
        void SetExcessPointsChooser(IExcessPointsChooser chooser);
        void SetJobCleaner(IJobCleaner jobCleaner);
        void SetJobSaver(IJobSaver jobSaver);
        void SetLogger(ILogger logger);
        void SetPointsRestorer(IPointRestorer pointRestorer);
        ExtraBackupJob GetJob();
    }
}