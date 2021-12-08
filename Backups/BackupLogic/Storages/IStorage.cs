using System.Collections.Generic;
using System.Collections.Specialized;

namespace Backups
{
    public interface IStorage
    {
        string StoragePath { get; }
        List<IJobObject> JobObjects { get; }
        void Process();
    }
}