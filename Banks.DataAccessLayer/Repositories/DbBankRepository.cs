using System.Collections.Generic;
using System.Data;
using System.Linq;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbBankRepository : IBankRepository
    {
        private BankContext _bankContext = BankContext.GetInstance;
        public void Add(BankModel model)
        {
            _bankContext.Banks.Add(model);
            _bankContext.SaveChanges();
        }

        public void Update(BankModel model)
        {
            _bankContext.Banks.Update(model);
            _bankContext.SaveChanges();
        }

        public BankModel GetModel(int id)
        {
            BankModel bankModel = _bankContext.Banks
                .Where(bank => bank.Id == id)
                .Select(bank => bank)
                .SingleOrDefault();
            if (bankModel == null)
                throw new DataException("Bank doesnt exist.");
            return bankModel;
        }

        public void Delete(int id)
        {
            _bankContext.Banks.Remove(GetModel(id));
            _bankContext.SaveChanges();
        }

        public List<BankModel> GetAll()
        {
            return _bankContext.Banks.Select(bank => bank).ToList();
        }
    }
}