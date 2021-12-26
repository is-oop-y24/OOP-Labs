using System.Collections.Generic;
using System.Linq;
using Backups;
using BackupsExtra.Services.Enumerables;
using Newtonsoft.Json;

namespace BackupsExtra.Services.Implementations.JobSavers.DTOs
{
    public class JobObjectDto
    {
        [JsonConstructor]
        public JobObjectDto(string name, JobObjectType jobObjectType, List<string> paths)
        {
            Name = name;
            JobObjectType = jobObjectType;
            Paths = paths;
        }

        public JobObjectDto(IJobObject jobObject)
        {
            Name = jobObject.Name;
            Paths = jobObject.Paths.ToList();
            switch (jobObject)
            {
                case JobFiles:
                    JobObjectType = JobObjectType.JobFiles;
                    break;
                default:
                    throw new BackupException("Incorrect job object type");
            }
        }

        public string Name { get; }
        public List<string> Paths { get; }
        public JobObjectType JobObjectType { get; }

        public IJobObject GetJobObject()
        {
            switch (JobObjectType)
            {
                case JobObjectType.JobFiles:
                    return new JobFiles(Paths, Name);
                default:
                    throw new BackupException("Incorrect job object type");
            }
        }
    }
}