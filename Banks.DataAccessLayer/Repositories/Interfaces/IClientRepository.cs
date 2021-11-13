using System.Collections.Generic;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Interfaces
{
    public interface IClientRepository : IRepository<ClientModel>
    {
        List<ClientModel> Find(int bankId);
    }
}