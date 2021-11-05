using System;
using Backups.FileSystem;
using Backups.Server.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Backups.Server
{
    public class OperationFactory : IOperationFactory
    {
        private readonly IBackupService _backupService;
        private readonly IFileRepository _fileRepository;

        public OperationFactory(IBackupService backupService, IFileRepository fileRepository)
        {
            _backupService = backupService;
            _fileRepository = fileRepository;
        }
        
        public IOperation GetOperation(Request request)
        {
            switch (request.RequestType)
            {
                case RequestType.MakeRestorePoint:
                    return new MakeRestorePointOperation(_backupService, request.RequestData);
                case RequestType.ObserveFile:
                    return new ObserveFileOperation(_backupService, request.RequestData);
                case RequestType.UploadFile:
                    return new UploadFileOperation(_fileRepository, request.RequestData);
                case RequestType.CreateJob:
                    return new CreateJobOperation(_backupService, request.RequestData);
                case RequestType.DeleteJobObject:
                    return new DeleteJobObjectOperation(_backupService, request.RequestData);
                default:
                    throw new ServerException("Incorrect operation type.");
            }
        }
    }
}