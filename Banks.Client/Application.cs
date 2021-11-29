using Banks.BusinessLogic.Data;
using Banks.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Spectre.Console.Cli;

namespace Banks
{
    public class Application
    {
        private readonly CommandApp _commandApp;
        private readonly IUserInterface _userInterface;

        public Application()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _userInterface = serviceCollection.BuildServiceProvider().GetService<IUserInterface>();
            var registrar = new TypeRegistrar(serviceCollection);
            _commandApp = new CommandApp(registrar);
            ConfigureCommands(_commandApp);
        }

        public void Run()
        {
            while (true)
            {
                _commandApp.Run(_userInterface.Read().Split());
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserInterface, ConsoleInterface>();
            services.AddScoped<ITableMaker, TableMaker>();
            services.AddDbContext<BankContext>();
            services.AddSingleton<IBankRepository, DbBankRepository>();
            services.AddSingleton<ICentralBank, CentralBank>();
        }

        private void ConfigureCommands(ICommandApp commandApp)
        {
            commandApp.Configure(config =>
            {
                config.AddCommand<RegisterBankCommand>("/register-bank");
                config.AddCommand<ShowBanksCommand>("/show-banks");
                config.AddCommand<RegisterClientCommand>("/register-client");
                config.AddCommand<IdentifyClientCommand>("/identify-client");
                config.AddCommand<ShowClientsCommand>("/show-clients");
                config.AddCommand<RegisterCreditAccountCommand>("/register-credit-account");
                config.AddCommand<RegisterDebitAccountCommand>("/register-debit-account");
                config.AddCommand<RegisterDepositAccountCommand>("/register-deposit-account");
                config.AddCommand<SubscribeToChangesCommand>("/subscribe-to-changes");
                config.AddCommand<TopUpCommand>("/top-up");
                config.AddCommand<TransferCommand>("/transfer");
                config.AddCommand<WithdrawCommand>("/withdraw");
                config.AddCommand<ShowAccountsCommand>("/show-accounts");
                config.AddCommand<AbortTransactionCommand>("/abort-transaction");
                config.AddCommand<RefreshCommand>("/refresh");
                config.AddCommand<MakePayoutsCommand>("/make-payouts");
                config.AddCommand<ShowTransactionsCommand>("/show-transactions");
            });
        }
    }
}