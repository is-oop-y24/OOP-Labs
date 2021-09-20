using System;
using Shops.Tools;

namespace Shops
{
    public class Address
    {
        private string _address;

        public Address(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ShopManagerException("Address cannot be empty.");
            _address = address;
        }

        public string GetAddressString()
        {
            return _address;
        }
    }
}