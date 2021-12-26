using System;
using System.Collections.Generic;
using Backups.FileSystem;
using BackupsExtra;

namespace Backups.Server
{
    [Serializable]
    public class RequestData
    {
        public string Path { get; init; }
        public BackupFile BackupFile { get; init; }
        public string JobName { get; init; }
        public JobConfig JobConfig { get; init; }
        public string ObjectName { get; init; }
        public StorageMode StorageMode { get; init; }
        public int RestorePointId { get; }
    }
}