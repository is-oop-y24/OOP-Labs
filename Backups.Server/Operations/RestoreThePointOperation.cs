using System.Linq;
using Backups.Server.Tools;
using BackupsExtra;

namespace Backups.Server
{
    public class RestoreThePointOperation : IOperation
    {
        private IBackupService _backupService;
        private RequestData _data;
        
        public RestoreThePointOperation(IBackupService backupService, RequestData data)
        {
            _backupService = backupService;
            _data = data;
        }

        public Response Execute()
        {
            try
            {
                IBackupJob job = _backupService.FindJob(_data.JobName);
                if (job is not ExtraBackupJob extraJob) 
                    throw new BackupException("Cannot restore the point of default job.");
                RestorePoint restorePoint = extraJob.GetRestorePoint(_data.RestorePointId);
                extraJob.RestoreThePoint(restorePoint);
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