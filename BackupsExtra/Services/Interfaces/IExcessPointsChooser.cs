using System.Collections.Generic;
using Backups;

namespace BackupsExtra.Services.Services
{
    public interface IExcessPointsChooser
    {
        List<RestorePoint> ChoosePoints(List<RestorePoint> restorePoints);
    }
}