using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class RestorePoint
    {
        private readonly List<IStorage> _storages;
        private readonly string _path;

        public RestorePoint(string destinationPath, List<IJobObject> jobObjects, IStoragePacker storagePacker, IFileRepository fileRepository)
        {
            Date = DateTime.Now;
            _path = Path.Combine(destinationPath, $"{Date.Day}.{Date.Month}.{Date.Year} [Time {Date.Hour}.{Date.Minute}.{Date.Second}]");
            _storages = storagePacker.MakeStorages(jobObjects, fileRepository, _path);
        }

        public DateTime Date { get; }
    }
}