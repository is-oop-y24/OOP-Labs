using System.Collections.Generic;
using System.Collections.Specialized;
using Backups.FileSystem;

namespace Backups
{
    public interface IStorage
    {
        string StoragePath { get; }
        List<IJobObject> JobObjects { get; }
        void Process(IFileRepository fileRepository);
    }
}