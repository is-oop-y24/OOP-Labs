using System.Collections.Generic;
using System.Linq;
using Banks.DataAccessLayer.Interfaces;
using Banks.DataAccessLayer.Models;

namespace Banks
{
    public class SystemLoader : ISystemLoader
    {
        private readonly IMapper _mapper;
        
        private readonly IBankRepository _bankRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public SystemLoader(IMapper mapper,
            IBankRepository bankRepository,
            IClientRepository clientRepository,
            ITransactionRepository transactionRepository,
            IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _bankRepository = bankRepository;
            _clientRepository = clientRepository;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        private List<Client> LoadClients(BankModel bankModel, List<IAccountOptions> optionsCreated)
        {
            var clients = new List<Client>();            
            foreach (ClientModel clientModel in _clientRepository.Find(bankModel))
            {
                var accounts = new List<Account>();
                foreach (AccountModel accountModel in _accountRepository.Find(clientModel))
                {
                    IAccountOptions accountOptions = optionsCreated.Find(opt => opt.Id == accountModel.Options.Id);
                    if (accountOptions == null)
                    {
                        accountOptions = _mapper.GetAccountOptions(accountModel.Options);
                        optionsCreated.Add(accountOptions);
                    }
                    accounts.Add(_mapper.GetAccount(accountModel, accountOptions));
                }
                clients.Add(_mapper.GetClient(clientModel, accounts));
            }

            return clients;
        }

        private List<Transaction> LoadTransactions(BankModel bankModel, List<Client> clients)
        {
            var transactions = new List<Transaction>();
            foreach (TransactionModel transactionModel in _transactionRepository.Find(bankModel))
            {
                Client source = clients.Single(client => client.Id == transactionModel.Source.Id);
                Client destination = clients.Single(client => client.Id == transactionModel.Destination.Id);
                transactions.Add(_mapper.GetTransaction(transactionModel, source, destination));
            }

            return transactions;
        }
        
        public ICentralBank Load()
        {
            var banks = new List<Bank>();
            var optionsCreated = new List<IAccountOptions>();
            foreach (BankModel bankModel in _bankRepository.GetAll())
            {
                List<Client> clients = LoadClients(bankModel, optionsCreated);
                List<Transaction> transactions = LoadTransactions(bankModel, clients);
                banks.Add(_mapper.GetBank(bankModel, clients, transactions));
            }

            return new CentralBank(banks);
        }

        public void Save(ICentralBank centralBank)
        {
            throw new System.NotImplementedException();
        }
    }
}