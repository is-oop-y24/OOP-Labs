using System;
using Banks.DataAccessLayer.Interfaces;

namespace Banks.DataAccessLayer.Models
{
    public class TransactionModel : IDbModel
    {
        public int Id { get; set; }
        public BankModel Bank { get; set; }
        public DateTime Date { get; set; }
        public AccountModel Source { get; set; }
        public AccountModel Destination { get; set; }
        public decimal Sum { get; set; }
    }
}