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
                BackupJob job = _backup.GetJob(_data.JobName);
                job.AddObject(new JobObject(new List<string> {_data.Path}, Path.GetFileName(_data.Path)));
            }
            catch (ServerException serverException)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = serverException});
            }
            catch (BackupException backupException)
            {
                return new Response(ResponseCode.Success, new ResponseData
                {
                    Exception = new ServerException("Backup exception occured.")
                });
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}