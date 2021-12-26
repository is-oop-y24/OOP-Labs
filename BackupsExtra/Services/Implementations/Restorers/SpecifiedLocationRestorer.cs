using System.Collections.Generic;
using System.IO;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.Restorers
{
    public class SpecifiedLocationRestorer : IPointRestorer
    {
        private readonly IFileRepository _repository;
        private readonly IUnarchiver _unarchiver;
        private readonly ILogger _logger;

        public SpecifiedLocationRestorer(IFileRepository repository, IUnarchiver unarchiver, ILogger logger, string restorePath)
        {
            _repository = repository;
            _unarchiver = unarchiver;
            _logger = logger;
            RestorePath = restorePath;
        }

        public string RestorePath { get; }

        public void RestoreThePoint(RestorePoint restorePoint)
        {
            foreach (IStorage storage in restorePoint.Storages)
            {
                BackupFile storageArchive = _repository.GetFile(storage.StoragePath);
                List<PathFile> pathFiles = _unarchiver.Unpack(storageArchive);
                foreach (PathFile pathFile in pathFiles)
                {
                    string fileName = pathFile.BackupFile.Name.Name;
                    _repository.AddFile(pathFile.BackupFile, Path.Combine(RestorePath, fileName));
                }
            }
        }
    }
}