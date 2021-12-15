using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Services.Services;

namespace BackupsExtra
{
    public class ExtraBackupService : IExtraBackupService
    {
        private IJobSaver _jobSaver;
        private List<ExtraBackupJob> _jobs = new List<ExtraBackupJob>();

        public ExtraBackupService(IJobSaver jobSaver)
        {
            _jobSaver = jobSaver;
        }

        public ExtraBackupJob CreateExtraJob(ExtraJobBuilder builder)
        {
            ExtraBackupJob job = builder.GetJob();
            _jobs.Add(job);
            return job;
        }

        public ExtraBackupJob FindJob(string jobName)
        {
            return _jobs
                .SingleOrDefault(job => job.Name == jobName);
        }

        public void Save(string path)
        {
            const string jobListFileName = "JobList";
            var serializer = new SerializerToFile();
            
            serializer.SerializeToFile(_jobs.Select(job => job.Name), Path.Combine(path, jobListFileName));
            _jobs.ForEach(job => _jobSaver.Save(job, path));
        }

        public void Load(string path)
        {
        }
    }
}