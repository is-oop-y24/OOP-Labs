using Backups;
using Backups.FileSystem;

namespace BackupsExtra
{
    public class ExtraBackupJob : BackupJob
    {

        public ExtraBackupJob(string destinationPath, string jobName, IFileRepository fileRepository, IStoragePacker storagePacker)
            : base(destinationPath, jobName, fileRepository, storagePacker)
        { }
        
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