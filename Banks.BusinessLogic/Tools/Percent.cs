namespace Banks.BusinessLogic.Tools
{
    public class Percent
    {
        public Percent(decimal percent)
        {
            if (percent <= 0)
                throw new BankException("Percent must be a positive number.");
            Value = percent;
        }

        public decimal Value { get; }
    }
}