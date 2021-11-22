using Microsoft.EntityFrameworkCore;

namespace Banks.BusinessLogic.Data
{
    public class BankContext : DbContext
    {
        public BankContext()
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<IntervalSequence> IntervalSequences { get; set; }
        public DbSet<PercentInterval> PercentIntervals { get; set; }
        public DbSet<CreditOptions> CreditOptions { get; set; }
        public DbSet<DebitOptions> DebitOptions { get; set; }
        public DbSet<DepositOptions> DepositOptions { get; set; }
        public DbSet<AccountOptions> AccountOptions { get; set; }
        public DbSet<ClientIdentifier> ClientIdentifiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = new JsonFile("config.json")["ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>()
                .HasMany("_clients")
                .WithOne("Bank");

            modelBuilder.Entity<Bank>()
                .HasMany("_accounts")
                .WithOne();
            
            modelBuilder.Entity<Bank>()
                .HasMany("_transactions")
                .WithOne();

            modelBuilder.Entity<Client>()
                .HasMany("_accounts")
                .WithOne("Client");
        }
    }
}