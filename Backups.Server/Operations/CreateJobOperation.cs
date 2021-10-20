using System;
using Backups.Server.Tools;

namespace Backups.Server
{
    public class CreateJobOperation : IOperation
    {
        private IBackup _backup;
        private RequestData _data;

        public CreateJobOperation(IBackup backup, RequestData requestData)
        {
            _backup = backup;
            _data = requestData;
        }
        
        public Response Execute()
        {
            try
            {
                
                _backup.CreateJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."),
                    _data.StorageMode);
            }
            catch (Exception exception)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = new ServerException(exception.Message)});
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}