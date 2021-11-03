using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class ObserveFileOperation : IOperation
    {
        private readonly IBackupService _backupService;
        private readonly RequestData _data;

        public ObserveFileOperation(IBackupService backupService, RequestData requestData)
        {
            _backupService = backupService;
            _data = requestData;
        }
            
        public Response Execute()
        {
            try
            {
                BackupJob job = _backupService.GetJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."));
                job.AddObject(new JobDirectory(new List<string> {_data.Path}, Path.GetFileName(_data.Path)));
            }
            catch (ServerException serverException)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = serverException});
            }
            catch (BackupException backupException)
            {
                return new Response(ResponseCode.Error, new ResponseData
                {
                    Exception = new ServerException("Backup error: " + backupException)
                });
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}