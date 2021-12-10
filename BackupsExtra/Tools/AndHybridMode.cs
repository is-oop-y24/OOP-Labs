using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class AndHybridMode : IHybridMode
    {
        public List<RestorePoint> MakeHybrid(List<RestorePoint> restorePoints1, List<RestorePoint> restorePoints2)
        {
            return restorePoints1
                .Intersect(restorePoints2)
                .ToList();
        }
    }
}