using Banks.DataAccessLayer.Interfaces;

namespace Banks.DataAccessLayer.Models
{
    public class ClientModel : IDbModel
    {
        public int Id { get; set; }
        public BankModel Bank { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
    }
}