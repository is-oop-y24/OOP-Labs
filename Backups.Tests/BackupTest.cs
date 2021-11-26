using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        private IBackupService _backupService;
        private IFileRepository _fileRepository;
        
        [SetUp]
        public void SetUp()
        {
            _fileRepository = new FileRepositoryMock();
            _backupService = new BackupService("", _fileRepository);
        }

        [Test]
        public void CreatePointsAndStoragesTest_PointsAndStoragesCreated()
        {
            const string jobName = "jobName";
            const string fileName1 = "file1";
            const string fileName2 = "file2";
            byte[] fileContent1 = new byte[] {1, 2, 3};
            byte[] fileContent2 = new byte[] {4, 5, 6};
            const string fileFolderPath = "";

            var file1 = new BackupFile(new FileName(fileName1), fileContent1);
            var file2 = new BackupFile(new FileName(fileName2), fileContent2);
            _fileRepository.AddFile(file1, fileFolderPath);
            _fileRepository.AddFile(file2, fileFolderPath);
            
            BackupJob job = _backupService.CreateJob(jobName, new SplitStoragePacker());
            job.AddObject(new JobFiles(new List<string>
            {
                Path.Combine(fileFolderPath, fileName1),
            }, fileName1));
            
            job.AddObject(new JobFiles(new List<string>
            {
                Path.Combine(fileFolderPath, fileName2),
            }, fileName2));
            
            job.MakeRestorePoint();
            job.DeleteObject(fileName2);
            // RestorePoint использует время в качестве имени файла
            // Чтобы имя файла не продублировалось, поставим задержку
            System.Threading.Thread.Sleep(1000);
            job.MakeRestorePoint();
            Assert.AreEqual(2, job.RestorePoints[0].Storages.Count);
            Assert.AreEqual(1, job.RestorePoints[1].Storages.Count);
        }

        [Test]
        public void MakeRestorePoint_StorageFilesCreatedAndAddedToRepo()
        {
            const string jobName = "jobName";
            const string jobPath = "jobDir";
            const string fileName1 = "file1";
            const string fileName2 = "file2";
            byte[] fileContent1 = new byte[] {1, 2, 3};
            byte[] fileContent2 = new byte[] {4, 5, 6};
            const string fileFolderPath = "";

            var file1 = new BackupFile(new FileName(fileName1), fileContent1);
            var file2 = new BackupFile(new FileName(fileName2), fileContent2);
            _fileRepository.AddFile(file1, fileFolderPath);
            _fileRepository.AddFile(file2, fileFolderPath);
            
            BackupJob job = _backupService.CreateJob(jobName, new SingleStoragePacker());
            job.AddObject(new JobFiles(new List<string>
            {
                Path.Combine(fileFolderPath, fileName1),
            }, fileName1));
            
            job.AddObject(new JobFiles(new List<string>
            {
                Path.Combine(fileFolderPath, fileName2),
            }, fileName2));
            
            job.MakeRestorePoint();
            // Если такого файла нет в репозитории, получим исключение
            _fileRepository.GetFile(job.RestorePoints[0].Storages[0].StoragePath);
        }
        
    }
}