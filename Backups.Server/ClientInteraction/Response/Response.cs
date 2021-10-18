using System;

namespace Backups.Server
{
    [Serializable]
    public class Response
    {
        public Response(ResponseCode responseCode, ResponseData responseData)
        {
            ResponseCode = responseCode;
            ResponseData = responseData;
        }
        
        public ResponseCode ResponseCode { get; }
        public ResponseData ResponseData { get; }
    }
}