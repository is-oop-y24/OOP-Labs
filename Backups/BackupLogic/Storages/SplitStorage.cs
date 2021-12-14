using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class SplitStorage : IStorage
    {
        private readonly IJobObject _jobObject;
        private readonly string _destinationPath;
        private readonly FileName _name;

        public SplitStorage(string destinationPath, IJobObject jobObject, FileName storageName)
        {
            _destinationPath = destinationPath;
            _name = storageName;
            _jobObject = jobObject;
        }

        public string StoragePath => Path.Combine(_destinationPath, _name.Name);
        public List<IJobObject> JobObjects => new List<IJobObject> { _jobObject };

        public void Process(IFileRepository fileRepository)
        {
            IArchiver archiver = new Archiver();
            foreach (string path in _jobObject.Paths)
            {
                archiver.AddFile(fileRepository.GetFile(path));
            }

            fileRepository.AddFile(archiver.MakeArchive(_name), _destinationPath);
        }
    }
}