using System;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class MakeRestorePointOperation : IOperation
    {
        private IBackup _backup;
        private RequestData _data;

        public MakeRestorePointOperation(IBackup backup, RequestData requestData)
        {
            _backup = backup;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                BackupJob job = _backup.GetJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."));
                job.MakeRestorePoint();
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