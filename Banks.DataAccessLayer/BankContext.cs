using Banks.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Banks.DataAccessLayer
{
    public class BankContext : DbContext
    {
        public DbSet<BankModel> Banks { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<AccountOptionsModel> AccountOptions { get; set; }
    }
}