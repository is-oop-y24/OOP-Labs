using System;
using Backups.Server.Tools;
using BackupsExtra.Services.Implementations.JobSavers;

namespace Backups.Server
{
    public class CreateJobOperation : IOperation
    {
        private IBackupService _backupService;
        private RequestData _data;

        public CreateJobOperation(IBackupService backupService, RequestData requestData)
        {
            _backupService = backupService;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                IJobBuilder jobBuilder = new JobSaver().GetBuilder(_data.JobConfig);
                _backupService.CreateJob(jobBuilder);
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