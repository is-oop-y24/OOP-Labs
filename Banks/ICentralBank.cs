namespace Banks
{
    public interface ICentralBank
    {
        void MakePayouts();
        Bank RegisterBank(string bankName);
        Bank FindBank(string bankName);
        Bank GetBank(BankId bankId);
    }
}