using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public interface IJobObject
    {
        string Name { get; }
        ReadOnlyCollection<string> GetPathList();
    }
}