using System.Collections.Generic;
using System.Data;
using System.Linq;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbAccountRepository : IAccountRepository
    {
        private BankContext _bankContext = BankContext.GetInstance;
        public void Add(AccountModel model)
        {
            _bankContext.Accounts.Add(model);
            _bankContext.SaveChanges();
        }

        public void Update(AccountModel model)
        {
            _bankContext.Accounts.Update(model);
            _bankContext.SaveChanges();
        }

        public AccountModel GetModel(int id)
        {
            AccountModel accountModel = _bankContext.Accounts
                .Where(account => account.Id == id)
                .Select(account => account)
                .SingleOrDefault();
            if (accountModel == null)
                throw new DataException("Account doesnt exist.");
            return accountModel;
        }

        public void Delete(int id)
        {
            _bankContext.Accounts.Remove(GetModel(id));
            _bankContext.SaveChanges();
        }

        public List<AccountModel> Find(ClientModel client)
        {
            return _bankContext.Accounts
                .Where(account => account.Client == client)
                .ToList();
        }
    }
}