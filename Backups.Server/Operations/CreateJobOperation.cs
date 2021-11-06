using System;
using Backups.Server.Tools;

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
                IStoragePacker storagePacker;
                switch (_data.StorageMode)
                {
                    case StorageMode.SingleStorage:
                        storagePacker = new SingleStoragePacker();
                        break;
                    case StorageMode.SplitStorage:
                        storagePacker = new SplitStoragePacker();
                        break;
                    default:
                        throw new BackupException("Storage mode is not supported");
                }
                _backupService.CreateJob(_data.JobName ?? throw new ServerException("Request must have JobName argument."),
                    storagePacker,
                    _data.Path);
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