using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.FileSystem;

namespace Backups
{
    public class Storage
    {
        private readonly List<JobObject> _jobObjects = new List<JobObject>();
        private readonly IFileRepository _fileRepository;
        private FileName _name;
        private readonly string _path;

        private Storage(string path, IFileRepository fileRepository, FileName storageName)
        {
            _path = path;
            _name = storageName;
            _fileRepository = fileRepository;
        }

        public Storage(string path, List<JobObject> jobObjects, IFileRepository fileRepository, FileName storageName)
            : this(path, fileRepository, storageName)
        {
            _jobObjects.InsertRange(0, jobObjects);
        }

        public Storage(string path, JobObject jobObject, IFileRepository fileRepository, FileName storageName)
            : this(path, fileRepository, storageName)
        {
            _jobObjects.Add(jobObject);
        }

        internal void Process()
        {
            IArchiver archiver = new Archiver();
            foreach (JobObject jobObject in _jobObjects)
            {
                archiver.AddFile(_fileRepository.GetFile(jobObject.Path));
            }
            _fileRepository.AddFile(archiver.MakeArchive(_name), _path);
        }
    }
}