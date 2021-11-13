using System.Collections.Generic;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbClientRepository : IClientRepository
    {
        public void Add(ClientModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ClientModel model)
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

        public List<ClientModel> Find(int bankId)
        {
            throw new System.NotImplementedException();
        }
    }
}