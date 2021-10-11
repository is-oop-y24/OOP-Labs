using System;
using System.Collections.Generic;
using System.IO;

namespace Backups
{
    public class BackupJob
    {
        private readonly List<JobObject> _objects = new List<JobObject>();
        private readonly List<RestorePoint> _restorePoints = new List<RestorePoint>();

        public BackupJob() { }
        
        public StorageMode StorageMode { get; set; } = StorageMode.SingleStorage;

        public void AddFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public void MakeRestorePoint()
        {
            throw new NotImplementedException();
        }
    }
}