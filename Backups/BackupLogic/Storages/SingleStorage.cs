using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.FileSystem;

namespace Backups
{
    public class SingleStorage : IStorage
    {
        private readonly string _destinationPath;
        private readonly FileName _name;

        public SingleStorage(string destinationPath, List<IJobObject> jobObjects, FileName storageName)
        {
            _destinationPath = destinationPath;
            _name = storageName;
            JobObjects = jobObjects;
        }

        public string StoragePath => Path.Combine(_destinationPath, _name.Name);
        public List<IJobObject> JobObjects { get; }

        public void Process(IFileRepository fileRepository)
        {
            IArchiver archiver = new Archiver();
            foreach (IJobObject jobObject in JobObjects)
            {
                foreach (string path in jobObject.Paths)
                {
                    archiver.AddFile(fileRepository.GetFile(path));
                }
            }

            fileRepository.AddFile(archiver.MakeArchive(_name), _destinationPath);
        }
    }
}