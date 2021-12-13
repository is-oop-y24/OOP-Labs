using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.ExcessPointsChoosers
{
    public class HybridPointChooser : IExcessPointsChooser
    {
        public HybridPointChooser(CountPointChooser countChooser, DatePointChooser dateChooser, IHybridMode hybridMode)
        {
            CountChooser = countChooser;
            DateChooser = dateChooser;
            HybridMode = hybridMode;
        }

        public CountPointChooser CountChooser { get; }
        public DatePointChooser DateChooser { get; }
        public IHybridMode HybridMode { get; }

        public List<RestorePoint> ChoosePoints(List<RestorePoint> restorePoints)
        {
            List<RestorePoint> countPoints = CountChooser.ChoosePoints(restorePoints);
            List<RestorePoint> datePoints = CountChooser.ChoosePoints(restorePoints);
            return HybridMode.MakeHybrid(countPoints, datePoints);
        }
    }
}