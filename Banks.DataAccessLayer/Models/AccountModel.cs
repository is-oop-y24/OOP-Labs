using Banks.DataAccessLayer.Interfaces;

namespace Banks.DataAccessLayer.Models
{
    public class AccountModel : IDbModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public BankModel Bank { get; set; }
        public AccountOptionsModel Options { get; set; }
        public bool Notify { get; set; }
        public decimal Sum { get; set; }
    }
}