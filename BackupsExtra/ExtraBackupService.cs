using System;
using System.Collections.Generic;
using System.IO;
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
            _jobs.Add(builder.GetJob());
            return job;
        }

        public void Save(string path)
        {
            foreach (ExtraBackupJob job in _jobs)
            {
                _jobSaver.Save(job, job.Path);
            }
        }

        public void Load(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}