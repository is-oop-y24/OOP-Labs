using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Banks.BusinessLogic.Data
{
    public class DbBankRepository : IBankRepository
    {
        private readonly BankContext _bankContext;

        public DbBankRepository(BankContext bankContext)
        {
            bankContext.ThrowIfNull(nameof(bankContext));
            _bankContext = bankContext;
            
            bankContext.Banks
                .Include("_clients")
                .Include("_accounts")
                .Include("_transactions")
                .Load();
            
            bankContext.Clients
                .Include("_accounts")
                .Include(client => client.Identifier)
                .Load();

            var unusedIdentifiers = _bankContext.ClientIdentifiers
                .Except(
                    _bankContext.Clients
                        .Select(client => client.Identifier)
                )
                .ToList();
            _bankContext.RemoveRange(unusedIdentifiers);

            bankContext.Accounts
                .Include(acc => acc.Options)
                .Load();

            bankContext.DepositOptions
                .Include(options => options.IntervalSequence)
                    .ThenInclude(sequence => sequence.PercentIntervals)
                .Load();
        }
        
        public void AddBank(Bank bank)
        {
            bank.ThrowIfNull(nameof(bank));
            _bankContext.Banks.Add(bank);
            _bankContext.SaveChanges();
        }

        public List<Bank> GetBanks()
        {
            return _bankContext.Banks.ToList();
        }

        public Bank GetBank(int id)
        {
            return GetBanks().FirstOrDefault(bank => bank.Id == id) 
                   ?? throw new BankException("Bank doesnt exist.");
        }

        public void Save()
        {
            _bankContext.SaveChanges();
        }
    }
}