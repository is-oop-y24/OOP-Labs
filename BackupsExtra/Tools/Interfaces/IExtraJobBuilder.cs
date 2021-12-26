using System.Collections;
using System.Collections.ObjectModel;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Services;

namespace BackupsExtra
{
    public interface IExtraJobBuilder : IJobBuilder
    {
        void SetExcessPointsChooser(IExcessPointsChooser chooser);
        void SetJobCleaner(IJobCleaner jobCleaner);
        void SetLogger(ILogger logger);
        void SetPointsRestorer(IPointRestorer pointRestorer);
        void SetRestorePoints(ReadOnlyCollection<RestorePoint> restorePoints);
        void SetJobObjects(ReadOnlyCollection<IJobObject> jobObjects);
    }
}