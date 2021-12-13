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

        [JsonConverter(typeof(StringEnumConverter))]
        public RepositoryType RepositoryType { get; set; }
        public string LocalRepositoryPath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public StorageMode StorageMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ExcessPointsChooseMode ExcessPointsChooseMode { get; set; }
        public TimeSpan DateChooserMaxPointAge { get; set; }
        public int CountChooserMaxCount { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public HybridMode HybridMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobCleaningMode JobCleaningMode { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public LoggingMode LoggingMode { get; set; }
        public LogWritingMode LogWritingMode { get; set; }
        public string FileLoggerFilePath { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RestoringMode RestoringMode { get; set; }
        public string RestoringPath { get; set; }
    }
}