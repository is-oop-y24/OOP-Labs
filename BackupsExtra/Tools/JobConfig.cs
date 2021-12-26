using System;
using Backups;
using BackupsExtra.Services.Enumerables;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackupsExtra
{
    public class JobConfig
    {
        public string DestinationPath { get; set; }
        public string JobName { get; set; }

        public RepositoryType RepositoryType { get; set; }
        public string LocalRepositoryPath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StorageMode StorageMode { get; set; }

        public JobType JobType { get; set; }

        public ExcessPointsChooseMode ExcessPointsChooseMode { get; set; }
        public TimeSpan DateChooserMaxPointAge { get; set; }
        public int CountChooserMaxCount { get; set; }

        public HybridMode HybridMode { get; set; }
        public JobCleaningMode JobCleaningMode { get; set; }

        public LoggingMode LoggingMode { get; set; }
        public LogWritingMode LogWritingMode { get; set; }
        public string FileLoggerFilePath { get; set; }

        public RestoringMode RestoringMode { get; set; }
        public string RestoringPath { get; set; }
    }
}