using System.Collections.Generic;
using Backups.FileSystem;

namespace Backups
{
    public interface IStoragePacker
    {
        public List<IStorage> MakeStorages(List<IJobObject> jobObjects, string path);
    }
}