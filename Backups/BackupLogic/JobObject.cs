using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class JobObject
    {
        private readonly List<string> _paths = new List<string>();

        public JobObject(List<string> paths, string name)
        {
            _paths.InsertRange(0, paths);
            Name = name;
        }

        public ReadOnlyCollection<string> Paths => _paths.AsReadOnly();
        public string Name { get; }
    }
}