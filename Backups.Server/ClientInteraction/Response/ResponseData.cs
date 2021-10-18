using System;
using Backups.FileSystem;

namespace Backups.Server
{
    [Serializable]
    public class ResponseData
    {
        public File File { get; init; }
        public Exception Exception { get; init; }
    }
}