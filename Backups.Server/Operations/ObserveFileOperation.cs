using System;
using System.Collections.Generic;
using System.IO;
using Backups.FileSystem;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class ObserveFileOperation : IOperation
    {
        private readonly IBackup _backup;
        private readonly RequestData _data;

        public ObserveFileOperation(IBackup backup, RequestData requestData)
        {
            _backup = backup;
            _data = requestData;
        }
            
        public Response Execute()
        {
            try
            {
                BackupJob job = _backup.GetJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."));
                job.AddObject(new JobObject(new List<string> {_data.Path}, Path.GetFileName(_data.Path)));
            }
            catch (Exception exception)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = new ServerException(exception.Message)});
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}