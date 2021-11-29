namespace Banks.BusinessLogic.Tools
{
    public class ClientBuilder : IClientBuilder
    {
        private string _name;
        private string _address;
        private string _passport;
        
        public void SetName(string name)
        {
            _name = name;
        }

        public void SetAddress(string address)
        {
            _address = address;
        }

        public void SetPassport(string passport)
        {
            _passport = passport;
        }

        public Client GetClient(Bank bank)
        {
            return new Client(_name, bank,
                new ClientIdentifier { Address = _address, Passport = _passport});
        }
    }
}