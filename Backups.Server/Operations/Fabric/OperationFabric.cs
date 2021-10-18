using System;
using Backups.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Backups.Server
{
    public class OperationFabric : IOperationFabric
    {
        private readonly ServiceProvider _services;

        public OperationFabric(ServiceProvider services)
        {
            _services = services;
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
                default:
                    throw new Exception("Incorrect operation type.");
            }
        }
    }
}