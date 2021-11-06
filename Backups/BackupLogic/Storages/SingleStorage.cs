using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.FileSystem;

namespace Backups
{
    public class SingleStorage : IStorage
    {
        private readonly List<IJobObject> _jobObjects;
        private readonly IFileRepository _fileRepository;
        private readonly string _path;
        private readonly FileName _name;

        public SingleStorage(string destinationPath, List<IJobObject> jobObjects, IFileRepository fileRepository, FileName storageName)
        {
            _path = Path.Combine(destinationPath, storageName.Name);
            _name = storageName;
            _fileRepository = fileRepository;
            _jobObjects = jobObjects;
        }

        public void Process()
        {
            IArchiver archiver = new Archiver();
            foreach (IJobObject jobObject in _jobObjects)
            {
                foreach (string path in jobObject.GetPathList())
                {
                    archiver.AddFile(_fileRepository.GetFile(path));
                }
            }

            _fileRepository.AddFile(archiver.MakeArchive(_name), _path);
        }
    }
}