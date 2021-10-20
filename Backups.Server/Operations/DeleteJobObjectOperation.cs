using System;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class DeleteJobObjectOperation : IOperation
    {
        private IBackup _backup;
        private RequestData _data;

        public DeleteJobObjectOperation(IBackup backup, RequestData requestData)
        {
            _backup = backup;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                BackupJob job = _backup.GetJob(_data.JobName);
                job.DeleteObject(_data.ObjectName);
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