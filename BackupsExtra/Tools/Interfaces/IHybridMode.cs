using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public interface IHybridMode
    {
        List<RestorePoint> MakeHybrid(List<RestorePoint> restorePoints1, List<RestorePoint> restorePoints2);
    }
}