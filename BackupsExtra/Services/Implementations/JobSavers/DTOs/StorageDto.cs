using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Backups;
using Backups.FileSystem;
using Newtonsoft.Json;

namespace BackupsExtra.Services.Implementations.JobSavers.DTOs
{
    public class StorageDto
    {
        [JsonConstructor]
        public StorageDto(string storagePath, StorageMode storageMode, List<JobObjectDto> jobObjects)
        {
            StoragePath = storagePath;
            StorageMode = storageMode;
            JobObjects = jobObjects;
        }

        public StorageDto(IStorage storage)
        {
            StoragePath = storage.StoragePath;
            JobObjects = storage.JobObjects
                .Select(jo => new JobObjectDto(jo))
                .ToList();

            switch (storage)
            {
                case SingleStorage:
                    StorageMode = StorageMode.SingleStorage;
                    break;
                case SplitStorage:
                    StorageMode = StorageMode.SplitStorage;
                    break;
                default:
                    throw new BackupException("Incorrect storage mode.");
            }
        }

        public string StoragePath { get; }
        public List<JobObjectDto> JobObjects { get; }
        public StorageMode StorageMode { get; }

        public IStorage GetStorage()
        {
            var jobObjects = JobObjects
                .Select(jo => jo.GetJobObject())
                .ToList();

            switch (StorageMode)
            {
                case StorageMode.SingleStorage:
                    return new SingleStorage(Path.GetDirectoryName(StoragePath), jobObjects, new FileName(Path.GetFileName(StoragePath)));
                case StorageMode.SplitStorage:
                    return new SplitStorage(Path.GetDirectoryName(StoragePath), jobObjects.Single(), new FileName(Path.GetFileName(StoragePath)));
                default:
                    throw new BackupException("Incorrect storage mode.");
            }
        }
    }
}