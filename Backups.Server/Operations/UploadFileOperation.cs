using System;
using Backups.FileSystem;
using Backups.Server.Tools;

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
                _fileRepository.AddFile(_data.File ?? throw new ServerException("Request must have File argument."),
                    _data.Path ?? throw new ServerException("Request must have JobName argument."));
            }
            catch (ServerException serverException)
            {
                return new Response(ResponseCode.Error, new ResponseData {Exception = serverException});
            }

            return new Response(ResponseCode.Success, new ResponseData());
        }
    }
}