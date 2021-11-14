using Banks.BusinessLogic.Tools;

namespace Banks
{
    public class Account
    {
        private IAccountOptions _options;
        private decimal _nextPayout = 0;

        public Account(Client client, IAccountOptions options)
        {
            Client = client;
            Options = options;
        }
        
        public int Id { get; init; }
        
        public decimal Sum { get; private set; }
        public Client Client { get; init; }
        public IAccountOptions Options
        {
            get => _options;
            set
            {
                if (value == null)
                    throw new BankException("Account options cannot be null.");
                _options = value;
                // TODO Notify logic
            }
        }
        
        
    }
}