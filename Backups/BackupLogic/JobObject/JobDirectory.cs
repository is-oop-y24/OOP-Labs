using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class JobDirectory : IJobObject
    {
        private readonly List<string> _paths;

        public JobDirectory(List<string> paths, string name)
        {
            _paths = paths;
            Name = name;
        }

        public string Name { get; }

        public ReadOnlyCollection<string> GetPathList()
        {
            return _paths.AsReadOnly();
        }
    }
}