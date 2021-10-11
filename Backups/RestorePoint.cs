using System;
using System.Collections.Generic;

namespace Backups
{
    public class RestorePoint
    {
        private readonly List<Storage> _storages = new List<Storage>();
        private readonly string _path;
        private int _storageId = 1;

        public RestorePoint(string path)
        {
            _path = path;
        }
        
        public DateTime Date { get; }

        public void AddStorage(List<JobObject> jobObjects)
        {
            var storage = new Storage(_path, new StorageId(_storageId++), jobObjects);
            storage.Process();
            _storages.Add(storage);
        }
    }
}