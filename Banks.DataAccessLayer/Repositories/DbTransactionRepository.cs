using System.Collections.Generic;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbTransactionRepository : ITransactionRepository
    {
        public void Add(TransactionModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TransactionModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<TransactionModel> Find(int bankId)
        {
            throw new System.NotImplementedException();
        }
    }
}