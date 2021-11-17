using Microsoft.EntityFrameworkCore;

namespace Banks.BusinessLogic.Data
{
    public class BankContext : DbContext
    {
        private static BankContext _instance;
        private BankContext()
        {
            Database.EnsureCreated();
        }
        
        public static BankContext GetInstance => _instance ??= new BankContext();
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<IntervalSequence> IntervalSequences { get; set; }
        public DbSet<PercentInterval> PercentIntervals { get; set; }
        public DbSet<CreditOptions> CreditOptions { get; set; }
        public DbSet<DebitOptions> DebitOptions { get; set; }
        public DbSet<DepositOptions> DepositOptions { get; set; }
        public DbSet<AccountOptions> AccountOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = new JsonFile("config.json")["ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}