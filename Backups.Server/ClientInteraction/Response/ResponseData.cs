using System;
using Backups.FileSystem;
using Backups.Server.Tools;

namespace Backups.Server
{
    [Serializable]
    public class ResponseData
    {
        public File File { get; init; }
        public ServerException Exception { get; init; }
    }
}