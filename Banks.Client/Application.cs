using Banks.BusinessLogic.Data;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Banks
{
    public class Application
    {
        private readonly CommandApp _commandApp;
        private readonly IUserInterface _userInterface;
        
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserInterface, ConsoleInterface>();
            services.AddDbContext<BankContext>();
            services.AddSingleton<IBankRepository, DbBankRepository>();
            services.AddSingleton<ICentralBank, CentralBank>();
        }

        private void ConfigureCommands(ICommandApp commandApp)
        {
            commandApp.Configure(config =>
            {
                
            });
        }
        
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
    }
}