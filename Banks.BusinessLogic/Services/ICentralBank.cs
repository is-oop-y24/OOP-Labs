namespace Banks
{
    public interface ICentralBank
    {
        void MakePayouts();
        Bank RegisterBank(string bankName);
        Bank FindBank(int bankId);
        Bank GetBank(int bankId);
    }
}