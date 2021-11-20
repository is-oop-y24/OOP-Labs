using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Tools;
using Kfc.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Banks.BusinessLogic.Data
{
    public class DbBankRepository : IBankRepository
    {
        private readonly BankContext _bankContext;

        public DbBankRepository(BankContext bankContext)
        {
            bankContext.ThrowIfNull(nameof(bankContext));
            _bankContext = bankContext; 
        }
        
        public void AddBank(Bank bank)
        {
            bank.ThrowIfNull(nameof(bank));
            _bankContext.Banks.Add(bank);
            _bankContext.SaveChanges();
        }

        public List<Bank> GetBanks()
        {
            return _bankContext.Banks
                .Include(bank => bank.Accounts)
                .Include(bank => bank.Clients)
                .Include(bank => bank.Transactions)
                .ToList();
        }

        public Bank GetBank(int id)
        {
            return GetBanks().Find(bank => bank.Id == id) 
                   ?? throw new BankException("Bank doesnt exist.");
        }

        public void Save()
        {
            _bankContext.SaveChanges();
        }
    }
}