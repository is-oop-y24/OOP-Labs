using System.Collections.Generic;
using Backups.FileSystem;

namespace Backups
{
    public class SplitStoragePacker : IStoragePacker
    {
        public List<IStorage> MakeStorages(List<IJobObject> jobObjects, string path)
        {
            var result = new List<IStorage>();
            int id = 1;
            foreach (IJobObject jobObject in jobObjects)
            {
                result.Add(new SplitStorage(path, jobObject, new FileName($"Storage{id++}")));
            }

            return result;
        }
    }
}