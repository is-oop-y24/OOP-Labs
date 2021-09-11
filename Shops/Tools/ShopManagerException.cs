using System;
using System.Runtime.Serialization;

namespace Shops.Tools
{
    [Serializable]
    public class ShopManagerException : Exception
    {
        
        public ShopManagerException() { }
        public ShopManagerException(string message) : base(message) { }
        public ShopManagerException(string message, Exception inner) : base(message, inner) { }

        protected ShopManagerException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}