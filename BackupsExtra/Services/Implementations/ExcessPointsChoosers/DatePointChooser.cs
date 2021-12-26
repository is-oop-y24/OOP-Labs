using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.ExcessPointsChoosers
{
    public class DatePointChooser : IExcessPointsChooser
    {
        public DatePointChooser(TimeSpan maxPointAge)
        {
            MaxPointAge = maxPointAge;
        }

        public TimeSpan MaxPointAge { get; }
        public DateTime LastAvailableTime => DateTime.Now - MaxPointAge;

        public List<RestorePoint> ChoosePoints(List<RestorePoint> restorePoints)
        {
            return restorePoints
                .Where(point => point.Date < LastAvailableTime)
                .ToList();
        }
    }
}