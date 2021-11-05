using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.FileSystem;

namespace Backups
{
    public class Storage
    {
        private readonly IJobObject _jobObject;
        private readonly IFileRepository _fileRepository;
        private readonly string _path;
        private readonly FileName _name;

        private Storage(string path, IFileRepository fileRepository, FileName storageName)
        {
            _path = path;
            _name = storageName;
            _fileRepository = fileRepository;
        }
        
        public Storage(string path, IJobObject jobObject, IFileRepository fileRepository, FileName storageName)
            : this(path, fileRepository, storageName)
        {
            _jobObject = jobObject;
        }

        internal void Process()
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