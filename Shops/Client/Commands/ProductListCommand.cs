using System;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shops.Commands
{
    public class ProductListCommand : Command<ProductListCommandSettings>
    {
        private readonly IShopManager _shopManager;
        private readonly IUserInterface _userInterface;

        public ProductListCommand(IShopManager shopManager, IUserInterface userInterface)
        {
            _shopManager = shopManager;
            _userInterface = userInterface;
        }

        public override int Execute(CommandContext context, ProductListCommandSettings settings)
        {
            var table = new Table();
            table.AddColumn("Product name");
            foreach (Product product in _shopManager.Products)
            {
                table.AddRow(product.Name);
            }
            _userInterface.ShowTable(table);
            return 0;
        }
    }
}