using System;
using System.Runtime.Serialization;

namespace Banks.BusinessLogic.Tools
{
    [Serializable]
    public class BankException : Exception
    {
        public BankException() { }
        public BankException(string message) : base(message) { }
        public BankException(string message, Exception inner) : base(message, inner) { }

        protected BankException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}