using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Interfaces
{
    public interface ITransactionRepository : IRepository<TransactionModel>
    {
        List<TransactionModel> Find(BankModel bankModel);
    }
}