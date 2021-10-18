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
        private readonly string _path;
        private FileName _name;

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

        private Storage(string path, IFileRepository fileRepository, FileName storageName)
        {
            _path = path;
            _name = storageName;
            _fileRepository = fileRepository;
        }

        internal void Process()
        {
            IArchiver archiver = new Archiver();
            foreach (JobObject jobObject in _jobObjects)
            {
                foreach (string path in jobObject.Paths)
                {
                    archiver.AddFile(_fileRepository.GetFile(path));
                }
            }

            _fileRepository.AddFile(archiver.MakeArchive(_name), _path);
        }
    }
}