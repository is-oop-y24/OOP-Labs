using System;
using Backups.FileSystem;
using Backups.Server.Tools;
using Microsoft.Extensions.DependencyInjection;

namespace Backups.Server
{
    public class OperationFactory : IOperationFactory
    {
        private readonly ServiceProvider _services;

        public OperationFactory(ServiceCollection servicesCollection)
        {
            _services = servicesCollection.BuildServiceProvider();
        }
        
        public IOperation GetOperation(Request request)
        {
            switch (request.RequestType)
            {
                case RequestType.MakeRestorePoint:
                    return new MakeRestorePointOperation(_services.GetService<IBackup>(), request.RequestData);
                case RequestType.ObserveFile:
                    return new ObserveFileOperation(_services.GetService<IBackup>(), request.RequestData);
                case RequestType.UploadFile:
                    return new UploadFileOperation(_services.GetService<IFileRepository>(), request.RequestData);
                case RequestType.CreateJob:
                    return new CreateJobOperation(_services.GetService<IBackup>(), request.RequestData);
                case RequestType.DeleteJobObject:
                    return new DeleteJobObjectOperation(_services.GetService<IBackup>(), request.RequestData);
                default:
                    throw new ServerException("Incorrect operation type.");
            }
        }
    }
}