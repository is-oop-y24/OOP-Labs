using System;
using Backups.FileSystem;

namespace Backups.Server
{
    public class UploadFileOperation : IOperation
    {
        private readonly RequestData _data;
        private readonly IFileRepository _fileRepository;
        public UploadFileOperation(IFileRepository fileRepository, RequestData data)
        {
            _data = data;
            _fileRepository = fileRepository;
        }
        
        public Response Execute()
        {
            try
            {
                _fileRepository.AddFile(_data.File, _data.Path);
            }
            catch (Exception exception)
            {
                return new Response(ResponseCode.Error, new ResponseData(){Exception = exception});
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}