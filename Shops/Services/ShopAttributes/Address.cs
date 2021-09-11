namespace Shops
{
    public class Address
    {
        private string _address;

        public Address(string address)
        {
            _address = address;
        }

        public string GetAddressString()
        {
            return _address;
        }
    }
}