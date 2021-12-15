using System;
using System.Collections.Generic;
using System.Text.Json;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Implementations.ExcessPointsChoosers;
using BackupsExtra.Services.Implementations.JobCleaners;
using BackupsExtra.Services.Implementations.JobSavers;
using BackupsExtra.Services.Implementations.Loggers;
using BackupsExtra.Services.Implementations.Restorers;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            IFileRepository repository = new LocalRepository(@"C:\Users\79148\Desktop\Test");
            var builder = new ExtraJobBuilder();
            builder.SetLogger(new ConsoleLogger(new TimeLogMessageMaker()));
            builder.SetDestinationPath(@"C:\Users\79148\Desktop\Test");
            builder.SetFileRepository(repository);
            builder.SetJobCleaner(new RemoveJobCleaner());
            builder.SetJobName("Job1");
            builder.SetPointsRestorer(new OriginalLocationRestorer(repository, new Unarchiver(), new ConsoleLogger(new TimeLogMessageMaker())));
            builder.SetStoragePacker(new SplitStoragePacker());
            builder.SetExcessPointsChooser(new DatePointChooser(new TimeSpan(10, 0, 0, 0)));

            IExtraBackupService service = new ExtraBackupService(new JobSaver());
            ExtraBackupJob job = service.CreateExtraJob(builder);
            repository.AddFile(new BackupFile(new FileName("File1"), new byte[] { 1, 2, 3 }), string.Empty);
            job.AddObject(new JobFiles(new List<string> { @"File1" }, "SomeObj"));
            job.MakeRestorePoint();
            service.Save(@"C:\Users\79148\Desktop\Test\Settings");
            var loadedJob = new JobSaver().Load(@"C:\Users\79148\Desktop\Test\Settings\Job1");
        }
    }
}
