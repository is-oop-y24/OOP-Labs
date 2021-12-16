using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups;
using BackupsExtra.Services.Services;

namespace BackupsExtra
{
    public class ExtraBackupService : IExtraBackupService
    {
        private const string _jobListFileName = "JobList";
        private const string _settingsDirectoryName = "Settings";

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
            var serializer = new SerializerToFile();

            serializer.SerializeToFile(_jobs.Select(job => job.Name), Path.Combine(path, _jobListFileName));
            _jobs.ForEach(job => _jobSaver.Save(job, path));
        }

        public void Load(string path)
        {
            var serializer = new SerializerToFile();

            List<string> jobNames = serializer.DeserializeFromFile<List<string>>(Path.Combine(path, _jobListFileName));
            foreach (string jobName in jobNames)
            {
                string jobPath = Path.Combine(path, jobName);
                ExtraBackupJob job = _jobSaver.Load(jobPath);
            }
        }
    }
}