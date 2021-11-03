using System;
using Backups.FileSystem;
using Isu;

namespace Backups.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileRepository localRepository = new LocalRepository(@"F:\repository");
            IBackupService backupService = new BackupService("", localRepository);
            new Server("127.0.0.1", 8888)
            {
                FileRepository = localRepository,
                BackupService = backupService,
                OperationFactory = new OperationFactory(backupService, localRepository),
                Logger = new ConsoleLogger(),
            }
                .Run();
        }
    }
}