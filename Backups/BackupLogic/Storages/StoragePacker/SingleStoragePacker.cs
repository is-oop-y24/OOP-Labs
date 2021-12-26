using System.Collections.Generic;
using Backups.FileSystem;

namespace Backups
{
    public class SingleStoragePacker : IStoragePacker
    {
        public List<IStorage> MakeStorages(List<IJobObject> jobObjects, string path)
        {
            var result = new List<IStorage> { new SingleStorage(path, jobObjects, new FileName("Storage1")) };
            return result;
        }
    }
}