using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.ExcessPointsChoosers
{
    public class CountPointChooser : IExcessPointsChooser
    {
        public CountPointChooser(int maxCount)
        {
            if (maxCount <= 0)
                throw new BackupException("Max count of points must be a positive integer");

            MaxCount = maxCount;
        }

        public int MaxCount { get; }

        public List<RestorePoint> ChoosePoints(List<RestorePoint> restorePoints)
        {
            return restorePoints
                .OrderBy(point => point.Date)
                .SkipLast(MaxCount)
                .ToList();
        }
    }
}