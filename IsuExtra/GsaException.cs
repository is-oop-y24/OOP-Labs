using System;
using System.Runtime.Serialization;

namespace IsuExtra
{
    [Serializable]
    public class GsaException : Exception
    {
        public GsaException() { }
        public GsaException(string message) : base(message) { }
        public GsaException(string message, Exception inner) : base(message, inner) { }

        protected GsaException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}