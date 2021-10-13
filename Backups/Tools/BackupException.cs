using System;
using System.Runtime.Serialization;

namespace Backups
{
    [Serializable]
    public class BackupException : Exception
    {
        public BackupException() { }
        public BackupException(string message) : base(message) { }
        public BackupException(string message, Exception inner) : base(message, inner) { }

        protected BackupException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}