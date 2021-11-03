using System;
using Backups.FileSystem;
using Backups.Server.Tools;

namespace Backups.Server
{
    [Serializable]
    public class ResponseData
    {
        public BackupFile BackupFile { get; init; }
        public ServerException Exception { get; init; }
    }
}