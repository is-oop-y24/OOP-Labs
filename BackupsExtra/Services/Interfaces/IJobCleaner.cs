using System.Collections.Generic;
using Backups;

namespace BackupsExtra.Services.Services
{
    public interface IJobCleaner
    {
        List<RestorePoint> GetCleanedList(List<RestorePoint> allPoints, List<RestorePoint> excessivePoints);
    }
}