using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class RestorePoint
    {
        private readonly List<IStorage> _storages;

        public RestorePoint(string destinationPath, List<IJobObject> jobObjects, IStoragePacker storagePacker, IFileRepository fileRepository)
        {
            Date = DateTime.Now;
            Path = System.IO.Path.Combine(destinationPath, $"{Date.Day}.{Date.Month}.{Date.Year} [Time {Date.Hour}.{Date.Minute}.{Date.Second}]");
            _storages = storagePacker.MakeStorages(jobObjects, fileRepository, Path);
        }

        public DateTime Date { get; }
        public ReadOnlyCollection<IStorage> Storages => _storages.AsReadOnly();
        public string Path { get; }

        public void Process()
        {
            _storages.ForEach(storage => storage.Process());
        }
    }
}