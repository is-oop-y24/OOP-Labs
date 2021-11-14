using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Interfaces
{
    public interface IAccountRepository : IRepository<AccountModel>
    {
        public List<AccountModel> Find(ClientModel client);
    }
}