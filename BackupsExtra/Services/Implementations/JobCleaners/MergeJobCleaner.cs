using System;
using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra.Services.Implementations.JobCleaners
{
    public class MergeJobCleaner : IJobCleaner
    {
        public List<RestorePoint> GetCleanedList(List<RestorePoint> allPoints, List<RestorePoint> excessivePoints)
        {
            RestorePoint excessivePoint = MergeList(excessivePoints);
            RestorePoint oldestPoint = allPoints.First(point => point.Date ==
                                                                 allPoints.Min(point => point.Date));
            var result = new List<RestorePoint>(allPoints);
            allPoints.Remove(oldestPoint);
            allPoints.Add(MergePoints(excessivePoint, oldestPoint));
            return result;
        }

        private RestorePoint MergeList(List<RestorePoint> points)
        {
            return points.Aggregate(MergePoints);
        }

        private RestorePoint MergePoints(RestorePoint point1, RestorePoint point2)
        {
            RestorePoint olderPoint = point1.Date < point2.Date ? point1 : point2;
            RestorePoint newerPoint = point1.Date < point2.Date ? point2 : point1;

            if (olderPoint.Storages[0] is SingleStorage || newerPoint.Storages[0] is SingleStorage)
                return newerPoint;

            var storages = new List<IStorage>();

            foreach (IStorage olderStorage in olderPoint.Storages)
            {
                IStorage newerStorage = newerPoint.Storages
                    .SingleOrDefault(newStorage => newStorage.JobObjects.Single() == olderStorage.JobObjects.Single());

                storages.Add(newerStorage ?? olderStorage);
            }

            return new RestorePoint(newerPoint.Date, newerPoint.Path, storages);
        }
    }
}