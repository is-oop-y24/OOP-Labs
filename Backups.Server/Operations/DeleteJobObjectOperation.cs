using System;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class DeleteJobObjectOperation : IOperation
    {
        private IBackupService _backupService;
        private RequestData _data;

        public DeleteJobObjectOperation(IBackupService backupService, RequestData requestData)
        {
            _backupService = backupService;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                BackupJob job = _backupService.GetJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."));
                job.DeleteObject(_data.ObjectName ?? throw new ServerException("Request must have JobObjectName argument."));
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