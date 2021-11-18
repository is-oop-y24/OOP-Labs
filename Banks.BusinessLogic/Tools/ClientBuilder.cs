using Kfc.Utility.Extensions;

namespace Banks.BusinessLogic.Tools
{
    public class ClientBuilder : IClientBuilder
    {
        private string _name;
        private string _address;
        private string _passport;
        private Bank _bank;

        public ClientBuilder(string name)
        {
            SetName(name);
        }

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

        public void SetBank(Bank bank)
        {
            _bank = bank.ThrowIfNull(nameof(bank));
        }

        public Client GetClient()
        {
            return new Client(_name, _bank, new ClientId{Address = _address, Passport = _passport});
        }
    }
}