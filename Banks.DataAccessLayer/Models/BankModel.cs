using Banks.DataAccessLayer.Interfaces;

namespace Banks.DataAccessLayer.Models
{
    public class BankModel  : IDbModel
    {
        public int Id { get; set; }
    }
}