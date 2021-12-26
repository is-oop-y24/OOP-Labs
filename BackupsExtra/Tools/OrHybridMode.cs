using System.Collections.Generic;
using System.Linq;
using Backups;

namespace BackupsExtra
{
    public class OrHybridMode : IHybridMode
    {
        public List<RestorePoint> MakeHybrid(List<RestorePoint> restorePoints1, List<RestorePoint> restorePoints2)
        {
            return restorePoints1
                .Union(restorePoints2)
                .ToList();
        }
    }
}