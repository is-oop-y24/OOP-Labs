using System.Collections.Generic;
using System.Data;
using System.Linq;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks.DataAccessLayer.Repositories
{
    public class DbClientRepository : IClientRepository
    {
        private BankContext _bankContext = BankContext.GetInstance;
        public void Add(ClientModel model)
        {
            _bankContext.Clients.Add(model);
            _bankContext.SaveChanges();
        }

        public void Update(ClientModel model)
        {
            _bankContext.Clients.Update(model);
            _bankContext.SaveChanges();
        }

        public ClientModel GetModel(int id)
        {
            ClientModel clientModel = _bankContext.Clients
                .Where(client => client.Id == id)
                .Select(client => client)
                .SingleOrDefault();
            if (clientModel == null)
                throw new DataException("Client doesnt exist.");
            return clientModel;
        }

        public void Delete(int id)
        {
            _bankContext.Clients.Remove(GetModel(id));
        }

        public List<ClientModel> Find(int bankId)
        {
            return _bankContext.Clients
                .Where(client => client.Bank.Id == bankId)
                .ToList();
        }
    }
}