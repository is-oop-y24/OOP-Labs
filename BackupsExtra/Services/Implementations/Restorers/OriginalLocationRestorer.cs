using System.Collections.Generic;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Restorers
{
    public class OriginalLocationRestorer : IPointRestorer
    {
        private readonly IFileRepository _repository;
        private readonly IUnarchiver _unarchiver;

        public OriginalLocationRestorer(IFileRepository repository, IUnarchiver unarchiver)
        {
            _repository = repository;
            _unarchiver = unarchiver;
        }

        public void RestoreThePoint(RestorePoint restorePoint)
        {
            foreach (IStorage storage in restorePoint.Storages)
            {
                BackupFile storageArchive = _repository.GetFile(storage.StoragePath);
                List<BackupFile> files = _unarchiver.Unpack(storageArchive);
                foreach (BackupFile file in files)
                {
                    IJobObject jobObject = storage.JobObjects.Find(obj => obj.Name == file.Name.Name);
                    _repository.AddFile(file, jobObject.GetPathList()[0]);
                }
            }
        }
    }
}