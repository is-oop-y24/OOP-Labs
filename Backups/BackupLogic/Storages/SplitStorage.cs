using System.IO;
using Backups.FileSystem;

namespace Backups
{
    public class SplitStorage : IStorage
    {
        private readonly IJobObject _jobObject;
        private readonly IFileRepository _fileRepository;
        private readonly string _path;
        private readonly FileName _name;

        public SplitStorage(string destinationPath, IJobObject jobObject, IFileRepository fileRepository, FileName storageName)
        {
            _path = Path.Combine(destinationPath, storageName.Name);
            _name = storageName;
            _fileRepository = fileRepository;
            _jobObject = jobObject;
        }

        public void Process()
        {
            IArchiver archiver = new Archiver();
            foreach (string path in _jobObject.GetPathList())
            {
                archiver.AddFile(_fileRepository.GetFile(path));
            }

            _fileRepository.AddFile(archiver.MakeArchive(_name), _path);
        }
    }
}