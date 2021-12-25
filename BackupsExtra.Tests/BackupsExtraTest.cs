using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backups;
using BackupsExtra.Services.Implementations.ExcessPointsChoosers;
using BackupsExtra.Services.Implementations.JobSavers;
using BackupsExtra.Services.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        [Test]
        public void CountExcessChooserGetExcessPoints_RightPointsNumberReturned()
        {
            const int maxCount = 3;
            const int currentCount = 5;
            IExcessPointsChooser chooser = new CountPointChooser(maxCount);

            const string pointPath = "";
            DateTime date = DateTime.Now;
            var storages = new List<IStorage>();

            var points = new List<RestorePoint>();
            foreach (int pointNum in Enumerable.Range(1, currentCount))
            {
                points.Add(new RestorePoint(date, pointPath, storages));
                date += TimeSpan.FromDays(1);
            }

            List<RestorePoint> excessPoints = chooser.ChoosePoints(points);
            Assert.AreEqual(points.Count - maxCount, excessPoints.Count);
        }

        [Test]
        public void DateExcessChooserGetExcessPoints_RightPointsNumReturned()
        {
            const int oldCount = 3;
            const int newCount = 3;
            var maxPeriod = TimeSpan.FromDays(1);
            DateTime oldDate = DateTime.Now - maxPeriod * 2;
            DateTime newDate = DateTime.Now - maxPeriod / 2;
            IExcessPointsChooser chooser = new DatePointChooser(maxPeriod);

            var points = new List<RestorePoint>();
            const string pointPath = "";
            var storages = new List<IStorage>();
            foreach (int pointNum in Enumerable.Range(1, oldCount))
            {
                points.Add(new RestorePoint(oldDate, pointPath, storages));
            }

            foreach (int pointNum in Enumerable.Range(1, newCount))
            {
                points.Add(new RestorePoint(newDate, pointPath, storages));
            }

            List<RestorePoint> excessPoints = chooser.ChoosePoints(points);
            Assert.AreEqual(oldCount, excessPoints.Count);
        }
    }
}