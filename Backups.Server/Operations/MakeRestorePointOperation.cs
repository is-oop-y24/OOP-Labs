using System;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class MakeRestorePointOperation : IOperation
    {
        private IBackupService _backupService;
        private RequestData _data;

        public MakeRestorePointOperation(IBackupService backupService, RequestData requestData)
        {
            _backupService = backupService;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                BackupJob job = _backupService.GetJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."));
                job.MakeRestorePoint();
            }
            catch (ServerException serverException)
            {
                return new Response(ResponseCode.Error, new ResponseData {Error = new Error{Message = serverException.Message}});
            }
            catch (BackupException backupException)
            {
                return new Response(ResponseCode.Error, new ResponseData
                {
                    Error = new Error { Message = "Backup error: " + backupException}
                });
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}