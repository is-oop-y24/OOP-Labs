using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Backups.FileSystem;

namespace Backups
{
    public class RestorePoint
    {
        private readonly List<IStorage> _storages;

        public RestorePoint(string destinationPath, List<IJobObject> jobObjects, IStoragePacker storagePacker)
        {
            Date = DateTime.Now;
            Path = System.IO.Path.Combine(destinationPath, $"{Date.Day}.{Date.Month}.{Date.Year} [Time {Date.Hour}.{Date.Minute}.{Date.Second}]");
            _storages = storagePacker.MakeStorages(jobObjects, Path);
        }

        public RestorePoint(DateTime date, string path, List<IStorage> storages)
        {
            Date = date;
            Path = System.IO.Path.Combine(path, $"{Date.Day}.{Date.Month}.{Date.Year} [Time {Date.Hour}.{Date.Minute}.{Date.Second}]");
            _storages = storages;
        }

        public DateTime Date { get; }
        public ReadOnlyCollection<IStorage> Storages => _storages.AsReadOnly();
        public string Path { get; }

        public void Process(IFileRepository fileRepository)
        {
            _storages.ForEach(storage => storage.Process(fileRepository));
        }
    }
}