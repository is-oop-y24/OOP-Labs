using System;
using System.Runtime.Serialization;

namespace Banks.DataAccessLayer.Tools
{
    [Serializable]
    public class BanksDataException : Exception
    {
        public BanksDataException() { }
        public BanksDataException(string message) : base(message) { }
        public BanksDataException(string message, Exception inner) : base(message, inner) { }

        protected BanksDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}