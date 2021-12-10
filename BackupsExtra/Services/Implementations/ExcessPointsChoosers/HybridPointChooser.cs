using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.ExcessPointsChoosers
{
    public class HybridPointChooser : IExcessPointsChooser
    {
        private readonly CountPointChooser _countChooser;
        private readonly DatePointChooser _dateChooser;
        private readonly IHybridMode _hybridMode;

        public HybridPointChooser(CountPointChooser countChooser, DatePointChooser dateChooser, IHybridMode hybridMode)
        {
            _countChooser = countChooser;
            _dateChooser = dateChooser;
            _hybridMode = hybridMode;
        }
        
        public List<RestorePoint> ChoosePoints(List<RestorePoint> restorePoints)
        {
            List<RestorePoint> countPoints = _countChooser.ChoosePoints(restorePoints);
            List<RestorePoint> datePoints = _countChooser.ChoosePoints(restorePoints);
            return _hybridMode.MakeHybrid(countPoints, datePoints);
        }
    }
}