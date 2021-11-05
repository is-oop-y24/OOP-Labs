using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Backups
{
    public class JobFile : IJobObject
    {
        private string _path;

        public JobFile(string path, string name)
        {
            _path = path;
            Name = name;
        }

        public string Name { get; }

        public ReadOnlyCollection<string> GetPathList()
        {
            return new List<string>{_path}.AsReadOnly();
        }
    }
}