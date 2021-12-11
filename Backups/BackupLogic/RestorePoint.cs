using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public RestorePoint(DateTime date, string path, List<IStorage> storages)
        {
            Type storageMode = storages.FirstOrDefault()?.GetType();
            foreach (IStorage storage in storages)
            {
                if (storage.GetType() != storageMode)
                    throw new BackupException("Incorrect storage list.");
            }
            Date = date;
            Path = System.IO.Path.Combine(path, $"{Date.Day}.{Date.Month}.{Date.Year} [Time {Date.Hour}.{Date.Minute}.{Date.Second}]");
            _storages = storages;
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