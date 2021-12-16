using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Restorers
{
    public class OriginalLocationRestorer : IPointRestorer
    {
        private readonly IFileRepository _repository;
        private readonly IUnarchiver _unarchiver;
        private readonly ILogger _logger;

        public OriginalLocationRestorer(IFileRepository repository, IUnarchiver unarchiver, ILogger logger)
        {
            _repository = repository;
            _unarchiver = unarchiver;
            _logger = logger;
        }

        public void RestoreThePoint(RestorePoint restorePoint)
        {
            foreach (IStorage storage in restorePoint.Storages)
            {
                BackupFile storageArchive = _repository.GetFile(storage.StoragePath);
                List<PathFile> pathFiles = _unarchiver.Unpack(storageArchive);

                foreach (PathFile pathFile in pathFiles)
                {
                    _repository.AddFile(pathFile.BackupFile, Path.GetDirectoryName(pathFile.Path));
                }
            }
        }
    }
}