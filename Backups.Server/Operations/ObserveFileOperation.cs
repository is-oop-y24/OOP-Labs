using System;
using System.Collections.Generic;

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
                job.AddObject(new JobObject(new List<string> {_data.Path}));
            }
            catch (Exception exception)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = exception});
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}