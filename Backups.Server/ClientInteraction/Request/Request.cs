using System;

namespace Backups.Server
{
    [Serializable]
    public class Request
    {
        public Request(RequestType requestType, RequestData requestData)
        {
            RequestType = requestType;
            RequestData = requestData;
        }
        
        public RequestType RequestType { get; }
        public RequestData RequestData { get; }
    }
}