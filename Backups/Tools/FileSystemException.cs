using System;
using System.Runtime.Serialization;

namespace Backups
{
    [Serializable]
    public class FileSystemException : Exception
    {
        public FileSystemException() { }
        public FileSystemException(string message) : base(message) { }
        public FileSystemException(string message, Exception inner) : base(message, inner) { }

        protected FileSystemException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}