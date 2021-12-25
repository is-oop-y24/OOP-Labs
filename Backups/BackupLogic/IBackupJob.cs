using System.Collections.ObjectModel;
using Backups.FileSystem;

namespace Backups
{
    public interface IBackupJob
    {
        string Path { get; }
        string Name { get; }
        IFileRepository FileRepository { get; }
        IStoragePacker StoragePacker { get; }
        ReadOnlyCollection<RestorePoint> RestorePoints { get; }
        ReadOnlyCollection<IJobObject> JobObjects { get; }

        void AddObject(IJobObject jobObject);
        void DeleteObject(string name);
        RestorePoint GetRestorePoint(int id);
        RestorePoint MakeRestorePoint();
    }
}