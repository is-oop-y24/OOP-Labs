using System.Collections.Generic;
using System.Data;
using System.Linq;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbTransactionRepository : ITransactionRepository
    {
        private BankContext _bankContext = BankContext.GetInstance;
        public void Add(TransactionModel model)
        {
            _bankContext.Transactions.Add(model);
            _bankContext.SaveChanges();
        }

        public void Update(TransactionModel model)
        {
            _bankContext.Transactions.Update(model);
            _bankContext.SaveChanges();
        }

        public TransactionModel GetModel(int id)
        {
            TransactionModel transactionModel = _bankContext.Transactions
                .Where(transaction => transaction.Id == id)
                .Select(transaction => transaction)
                .SingleOrDefault();
            if (transactionModel == null)
                throw new DataException("Transaction doesnt exist.");
            return transactionModel;
        }

        public void Delete(int id)
        {
            _bankContext.Transactions.Remove(GetModel(id));
        }

        public List<TransactionModel> Find(int bankId)
        {
            return _bankContext.Transactions
                .Where(transaction => transaction.Bank.Id == bankId)
                .ToList();
        }
    }
}