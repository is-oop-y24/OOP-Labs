namespace Banks.BusinessLogic.Tools
{
    public interface IClientBuilder
    {
        void SetName(string name);
        void SetAddress(string address);
        void SetPassport(string passport); 
        Client GetClient(Bank bank);
    }
}