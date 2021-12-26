using System;
using Backups.FileSystem;
using BackupsExtra;
using BackupsExtra.Services.Implementations.JobSavers;

namespace Backups.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileRepository localRepository = new LocalRepository(@"F:\repository");
            IBackupService backupService = new ExtraBackupService(new JobSaver());
            new Server("127.0.0.1", 8888)
            {
                FileRepository = localRepository,
                BackupService = backupService,
                OperationFactory = new OperationFactory(localRepository, backupService),
                Logger = new ConsoleLogger(),
            }
                .Run();
        }
    }
}