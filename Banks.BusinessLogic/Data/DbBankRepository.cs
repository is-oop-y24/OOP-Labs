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

            _bankContext.DepositOptions
                .Include(options => options.IntervalSequence)
                    .ThenInclude(intervals => intervals.PercentIntervals)
                .Load();

            _bankContext.Banks
                .Include(bank => bank.Accounts)
                    .ThenInclude(account => account.Options)
                .Include(bank => bank.Clients)
                    .ThenInclude(client => client.Identifier)
                .Include(bank => bank.Transactions)
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