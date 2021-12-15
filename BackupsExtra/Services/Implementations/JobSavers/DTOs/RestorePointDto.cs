using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using Newtonsoft.Json;

namespace BackupsExtra.Services.Implementations.JobSavers.DTOs
{
    public class RestorePointDto
    {
        [JsonConstructor]
        public RestorePointDto(DateTime date, List<StorageDto> storages, string fullPath)
        {
            Date = date;
            Storages = storages;
            FullPath = fullPath;
        }

        public RestorePointDto(RestorePoint restorePoint)
        {
            Date = restorePoint.Date;
            FullPath = restorePoint.Path;
            Storages = restorePoint.Storages
                .Select(s => new StorageDto(s))
                .ToList();
        }

        public DateTime Date { get; }
        public List<StorageDto> Storages { get; }
        public string FullPath { get; }

        public RestorePoint GetRestorePoint()
        {
            return new (Date, Path.GetDirectoryName(FullPath), Storages.Select(sd => sd.GetStorage()).ToList());
        }
    }
}