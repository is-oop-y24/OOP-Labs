using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class RestorePoint
    {
        private readonly List<Storage> _storages = new List<Storage>();
        private readonly IFileRepository _fileRepository;
        private readonly string _path;
        private int _currentStorageId = 1;

        public RestorePoint(string path, IFileRepository fileRepository)
        {
            _path = path;
            _fileRepository = fileRepository;
            Date = DateTime.Now;
        }

        public DateTime Date { get; }

        public void AddStorage(IJobObject jobObject)
        {
            var storage = new Storage(_path, jobObject, _fileRepository, new FileName(_currentStorageId++.ToString()));
            storage.Process();
            _storages.Add(storage);
        }
    }
}