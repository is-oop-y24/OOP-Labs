using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.JobCleaners
{
    public class RemoveJobCleaner : IJobCleaner
    {
        public List<RestorePoint> GetCleanedList(List<RestorePoint> allPoints, List<RestorePoint> excessivePoints)
        {
            return allPoints
                .Except(excessivePoints)
                .ToList();
        }
    }
}