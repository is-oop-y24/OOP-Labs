using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using Backups;
using Backups.FileSystem;
using BackupsExtra.Services.Enumerables;
using BackupsExtra.Services.Implementations.ExcessPointsChoosers;
using BackupsExtra.Services.Implementations.JobCleaners;
using BackupsExtra.Services.Implementations.JobSavers.DTOs;
using BackupsExtra.Services.Implementations.Loggers;
using BackupsExtra.Services.Implementations.Restorers;
using BackupsExtra.Services.Services;
using Kfc.Utility.Extensions;
using Newtonsoft.Json;

namespace BackupsExtra.Services.Implementations.JobSavers
{
    public class JobSaver : IJobSaver
    {
        private const string _configFileName = "config.json";
        private const string _restorePointsFileName = "restorePoints.json";
        private const string _jobObjectsFileName = "jobObjects.json";

        private readonly SerializerToFile _serializer = new SerializerToFile();

        private IFileRepository _fileRepository;
        private ILogger _logger;

        public void Save(IBackupJob backupJob, string path)
        {
            string jobPath = Path.Combine(path, backupJob.Name);
            Directory.CreateDirectory(jobPath);
            SaveConfig(backupJob, jobPath);

            var restorePointDtos = backupJob.RestorePoints
                .Select(point => new RestorePointDto(point))
                .ToList();

            var jobObjectDtos = backupJob.JobObjects
                .Select(jo => new JobObjectDto(jo))
                .ToList();

            _serializer.SerializeToFile(restorePointDtos, Path.Combine(jobPath, _restorePointsFileName));
            _serializer.SerializeToFile(jobObjectDtos, Path.Combine(jobPath, _jobObjectsFileName));
        }

        public IBackupJob Load(string jobSettingsPath)
        {
            IJobBuilder builder = LoadConfig(Path.Combine(jobSettingsPath, _configFileName));

            LoadExtraData(jobSettingsPath, builder);
            return builder.GetJob();
        }

        public JobConfig GetConfig(IBackupJob backupJob)
        {
            var config = new JobConfig();
            config.DestinationPath = Path.GetDirectoryName(backupJob.Path);
            config.JobName = backupJob.Name;
            ConfigureJobType(backupJob, config);

            if (backupJob is not ExtraBackupJob extraJob) return config;
            ConfigureLogger(extraJob.Logger, config);
            ConfigureRepository(extraJob.FileRepository, config);
            ConfigureJobCleaner(extraJob.JobCleaner, config);
            ConfigurePointRestorer(extraJob.PointRestorer, config);
            ConfigureStoragePacker(extraJob.StoragePacker, config);
            ConfigureExcessPointsChooser(extraJob.ExcessPointsChooser, config);
            return config;
        }

        public IJobBuilder GetBuilder(JobConfig config)
        {
            config.ThrowIfNull(nameof(config));
            var builder = ResolveBuilder(config);
            builder.SetDestinationPath(config.DestinationPath);
            builder.SetJobName(config.JobName);

            if (builder is not IExtraJobBuilder extraBuilder) return builder;
            ResolveLogger(config, extraBuilder);
            ResolveRepository(config, extraBuilder);
            ResolveJobCleaner(config, extraBuilder);
            ResolvePointRestorer(config, extraBuilder);
            ResolveStoragePacker(config, extraBuilder);
            ResolveExcessPointsChooser(config, extraBuilder);
            return extraBuilder;
        }

        private void LoadExtraData(string jobSettingsPath, IJobBuilder builder)
        {
            if (builder is not IExtraJobBuilder extraBuilder) return;

            List<JobObjectDto> jobObjectDtos =
                _serializer.DeserializeFromFile<List<JobObjectDto>>(Path.Combine(jobSettingsPath, _jobObjectsFileName));
            List<RestorePointDto> restorePointsDtos =
                _serializer.DeserializeFromFile<List<RestorePointDto>>(Path.Combine(jobSettingsPath, _restorePointsFileName));

            var jobObjects = jobObjectDtos
                .Select(jod => jod.GetJobObject())
                .ToList();

            var restorePoints = restorePointsDtos
                .Select(rpd => rpd.GetRestorePoint())
                .ToList();

            extraBuilder.SetJobObjects(jobObjects.AsReadOnly());
            extraBuilder.SetRestorePoints(restorePoints.AsReadOnly());
        }

        private void SaveConfig(IBackupJob backupJob, string path)
        {
            JobConfig config = GetConfig(backupJob);
            string configPath = Path.Combine(path, _configFileName);
            _serializer.SerializeToFile(config, configPath);
        }

        private IJobBuilder LoadConfig(string path)
        {
            JobConfig config = _serializer.DeserializeFromFile<JobConfig>(path);
            return GetBuilder(config);
        }

        private IJobBuilder ResolveBuilder(JobConfig config)
        {
            switch (config.JobType)
            {
                case JobType.Default:
                    return new JobBuilder();
                case JobType.Extra:
                    return new ExtraJobBuilder();
                default:
                    throw new BackupException("Job type is not supported");
            }
        }

        private IHybridMode ResolveHybridMode(JobConfig config)
        {
            switch (config.HybridMode)
            {
                case HybridMode.And:
                    return new AndHybridMode();
                case HybridMode.Or:
                    return new OrHybridMode();
                default:
                    throw new BackupException("Incorrect hybrid mode.");
            }
        }

        private void ResolveExcessPointsChooser(JobConfig config, IExtraJobBuilder builder)
        {
            IExcessPointsChooser chooser;

            switch (config.ExcessPointsChooseMode)
            {
                case ExcessPointsChooseMode.Count:
                    chooser = new CountPointChooser(config.CountChooserMaxCount);
                    break;
                case ExcessPointsChooseMode.Date:
                    chooser = new DatePointChooser(config.DateChooserMaxPointAge);
                    break;
                case ExcessPointsChooseMode.Hybrid:
                    chooser = new HybridPointChooser(
                        new CountPointChooser(config.CountChooserMaxCount),
                        new DatePointChooser(config.DateChooserMaxPointAge),
                        ResolveHybridMode(config));
                    break;
                default:
                    throw new BackupException("Incorrect excess points choosing mode.");
            }

            builder.SetExcessPointsChooser(chooser);
        }

        private void ResolveStoragePacker(JobConfig config, IExtraJobBuilder builder)
        {
            IStoragePacker storagePacker;

            switch (config.StorageMode)
            {
                case StorageMode.SingleStorage:
                    storagePacker = new SingleStoragePacker();
                    break;
                case StorageMode.SplitStorage:
                    storagePacker = new SplitStoragePacker();
                    break;
                default:
                    throw new BackupException("Incorrect storage mode.");
            }

            builder.SetStoragePacker(storagePacker);
        }

        private void ResolvePointRestorer(JobConfig config, IExtraJobBuilder builder)
        {
            IPointRestorer pointRestorer;

            if (_fileRepository == null)
                throw new BackupException("Resolve repository first.");
            if (_logger == null)
                throw new BackupException("Resolve logger first.");

            switch (config.RestoringMode)
            {
                case RestoringMode.OriginalLocation:
                    pointRestorer = new OriginalLocationRestorer(_fileRepository, new Unarchiver(), _logger);
                    break;
                case RestoringMode.SpecifiedLocation:
                    pointRestorer = new SpecifiedLocationRestorer(_fileRepository, new Unarchiver(), _logger, config.RestoringPath);
                    break;
                default:
                    throw new BackupException("Incorrect point restoring mode.");
            }

            builder.SetPointsRestorer(pointRestorer);
        }

        private void ResolveJobCleaner(JobConfig config, IExtraJobBuilder builder)
        {
            IJobCleaner jobCleaner;

            switch (config.JobCleaningMode)
            {
                case JobCleaningMode.Merge:
                    jobCleaner = new MergeJobCleaner();
                    break;
                case JobCleaningMode.Remove:
                    jobCleaner = new RemoveJobCleaner();
                    break;
                default:
                    throw new BackupException("Incorrect job cleaning mode.");
            }

            builder.SetJobCleaner(jobCleaner);
        }

        private void ResolveRepository(JobConfig config, IExtraJobBuilder builder)
        {
            switch (config.RepositoryType)
            {
                case RepositoryType.Local:
                    _fileRepository = new LocalRepository(config.LocalRepositoryPath);
                    break;
                default:
                    throw new BackupException("Incorrect repository type.");
            }

            builder.SetFileRepository(_fileRepository);
        }

        private ILogger InstanceLogger(JobConfig config)
        {
            ILogMessageMaker messageMaker;

            switch (config.LogWritingMode)
            {
                case LogWritingMode.Default:
                    messageMaker = new DefaultLogMessageMaker();
                    break;
                case LogWritingMode.Time:
                    messageMaker = new TimeLogMessageMaker();
                    break;
                default:
                    throw new BackupException("Incorrect message maker mode.");
            }

            switch (config.LoggingMode)
            {
                case LoggingMode.Console:
                    _logger = new ConsoleLogger(messageMaker);
                    break;
                case LoggingMode.File:
                    _logger = new FileLogger(messageMaker, config.FileLoggerFilePath);
                    break;
                default:
                    throw new BackupException("Incorrect logging mode.");
            }

            return _logger;
        }

        private void ResolveLogger(JobConfig config, IExtraJobBuilder builder)
        {
            builder.SetLogger(InstanceLogger(config));
        }

        private void ConfigureRepository(IFileRepository fileRepository, JobConfig config)
        {
            switch (fileRepository)
            {
                case LocalRepository localRepository:
                    config.RepositoryType = RepositoryType.Local;
                    config.LocalRepositoryPath = localRepository.RepositoryPath;
                    break;
            }
        }

        private void ConfigureStoragePacker(IStoragePacker storagePacker, JobConfig config)
        {
            switch (storagePacker)
            {
                case SingleStoragePacker:
                    config.StorageMode = StorageMode.SingleStorage;
                    break;
                case SplitStoragePacker:
                    config.StorageMode = StorageMode.SplitStorage;
                    break;
            }
        }

        private void ConfigureExcessPointsChooser(IExcessPointsChooser excessPointsChooser, JobConfig config)
        {
            switch (excessPointsChooser)
            {
                case DatePointChooser datePointChooser:
                    config.ExcessPointsChooseMode = ExcessPointsChooseMode.Date;
                    config.DateChooserMaxPointAge = datePointChooser.MaxPointAge;
                    break;
                case CountPointChooser countPointChooser:
                    config.ExcessPointsChooseMode = ExcessPointsChooseMode.Count;
                    config.CountChooserMaxCount = countPointChooser.MaxCount;
                    break;
                case HybridPointChooser hybridPointChooser:
                    config.ExcessPointsChooseMode = ExcessPointsChooseMode.Hybrid;
                    config.DateChooserMaxPointAge = hybridPointChooser.DateChooser.MaxPointAge;
                    config.CountChooserMaxCount = hybridPointChooser.CountChooser.MaxCount;
                    switch (hybridPointChooser.HybridMode)
                    {
                        case AndHybridMode andHybridMode:
                            config.HybridMode = HybridMode.And;
                            break;
                        case OrHybridMode orHybridMode:
                            config.HybridMode = HybridMode.Or;
                            break;
                    }

                    break;
            }
        }

        private void ConfigureJobCleaner(IJobCleaner jobCleaner, JobConfig config)
        {
            switch (jobCleaner)
            {
                case RemoveJobCleaner:
                    config.JobCleaningMode = JobCleaningMode.Remove;
                    break;
                case MergeJobCleaner:
                    config.JobCleaningMode = JobCleaningMode.Merge;
                    break;
            }
        }

        private ILogMessageMaker GetLogMessageMaker(LogWritingMode logWritingMode)
        {
            switch (logWritingMode)
            {
                case LogWritingMode.Default:
                    return new DefaultLogMessageMaker();
                case LogWritingMode.Time:
                    return new TimeLogMessageMaker();
                default:
                    throw new BackupException("Incorrect log writing mode.");
            }
        }

        private LogWritingMode ConfigureLogWritingMode(ILogMessageMaker messageMaker)
        {
            switch (messageMaker)
            {
                case DefaultLogMessageMaker:
                    return LogWritingMode.Default;
                case TimeLogMessageMaker:
                    return LogWritingMode.Time;
                default:
                    throw new BackupException("Incorrect message maker type.");
            }
        }

        private void ConfigureJobType(IBackupJob backupJob, JobConfig config)
        {
            switch (backupJob)
            {
                case BackupJob:
                    config.JobType = JobType.Default;
                    break;
                case ExtraBackupJob:
                    config.JobType = JobType.Extra;
                    break;
                default:
                    throw new BackupException("Job type is not supported.");
            }
        }

        private void ConfigureLogger(ILogger logger, JobConfig config)
        {
            config.LogWritingMode = ConfigureLogWritingMode(logger.MessageMaker);

            switch (logger)
            {
                case ConsoleLogger:
                    config.LoggingMode = LoggingMode.Console;
                    break;
                case FileLogger fileLogger:
                    config.LoggingMode = LoggingMode.File;
                    config.FileLoggerFilePath = fileLogger.LogFilePath;
                    break;
            }
        }

        private void ConfigurePointRestorer(IPointRestorer pointRestorer, JobConfig config)
        {
            switch (pointRestorer)
            {
                case OriginalLocationRestorer:
                    config.RestoringMode = RestoringMode.OriginalLocation;
                    break;
                case SpecifiedLocationRestorer specifiedLocationRestorer:
                    config.RestoringMode = RestoringMode.SpecifiedLocation;
                    config.RestoringPath = specifiedLocationRestorer.RestorePath;
                    break;
            }
        }
    }
}