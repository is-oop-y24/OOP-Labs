using Banks.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Banks.DataAccessLayer
{
    public class BankContext : DbContext
    {
        private static BankContext _instance;
        private BankContext()
        {
            Database.EnsureCreated();
        }
        
        public static BankContext GetInstance => _instance ??= new BankContext();
        
        public DbSet<BankModel> Banks { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<AccountOptionsModel> AccountOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = new JsonFile("config.json")["ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}