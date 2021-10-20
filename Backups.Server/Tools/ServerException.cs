using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Backups.Server.Tools
{
    [Serializable]
    public class ServerException : Exception
    {
        public ServerException() { }
        public ServerException(string message) : base(message) { }
        public ServerException(string message, Exception inner) : base(message, inner) { }

        protected ServerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}