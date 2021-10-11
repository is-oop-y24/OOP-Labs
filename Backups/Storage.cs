using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class Storage
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();
        private readonly string _path;
        private readonly StorageId _id;

        public Storage(string backupPath, StorageId storageId, List<JobObject> jobObjects)
        {
            _path = backupPath;
            _id = storageId;
            _jobObjects.InsertRange(0, jobObjects);
        }

        public void Process()
        {
            throw new NotImplementedException();
        }
    }
}