using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Interfaces
{
    public interface IBankRepository : IRepository<BankModel>
    {
        List<BankModel> GetAll();
    }
}