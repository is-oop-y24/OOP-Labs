using System.Collections.ObjectModel;
using Backups.FileSystem;

namespace Backups
{
    public interface IBackupJob
    {
        public string Path { get; }
        public string Name { get; }
        public IFileRepository FileRepository { get; }
        public IStoragePacker StoragePacker { get; }
        public ReadOnlyCollection<RestorePoint> RestorePoints { get; }
        public ReadOnlyCollection<IJobObject> JobObjects { get; }

        void AddObject(IJobObject jobObject);
        void DeleteObject(string name);
        RestorePoint MakeRestorePoint();
    }
}