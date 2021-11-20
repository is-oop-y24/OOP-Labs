using Banks.BusinessLogic.Data;
using Banks.Commands;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<BankContext>();
            services.AddScoped<IBankRepository, DbBankRepository>();
            services.AddScoped<ICentralBank, CentralBank>();
            services.AddScoped<ITableMaker, TableMaker>();
        }

        private void ConfigureCommands(ICommandApp commandApp)
        {
            commandApp.Configure(config =>
            {
                config.AddCommand<RegisterBankCommand>("/register-bank");
                config.AddCommand<ShowBanksCommand>("/show-banks");
                config.AddCommand<RegisterClientCommand>("/register-client");
                config.AddCommand<AbortTransactionCommand>("/abort-transaction");
                config.AddCommand<IdentifyClientCommand>("/identify-client");
                config.AddCommand<MakePayoutsCommand>("/make-payouts");
                config.AddCommand<RefreshCommand>("/refresh");
                config.AddCommand<ShowAccountsCommand>("/show-accounts");
                config.AddCommand<ShowClientsCommand>("/show-clients");
                config.AddCommand<ShowTransactionsCommand>("/show-transactions");
                config.AddCommand<SubscribeToChangesCommand>("/subscribe-to-changes");
                config.AddCommand<TopUpCommand>("/top-up");
                config.AddCommand<TransferCommand>("/transfer");
                config.AddCommand<WithdrawCommand>("/withdraw");
            });
        }
    }
}