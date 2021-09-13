using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTests
    {
        private IShopManager _shopManager;
        
        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void MakeSupply_ProductsAreAvailableToBuy()
        {
            const int milkCount = 5;
            const int milkWorth = 30;
            const int breadCount = 10;
            const int breadWorth = 50;
            const int customerBalance = 200;
            const int productsToBuy = 2;

            Shop shop = _shopManager.CreateShop("Lenta", new Address("Belorusskaya"));
            Product milk = _shopManager.RegisterProduct("milk");
            Product bread = _shopManager.RegisterProduct("bread");
            
            var supply = new Supply();
            supply.AddProduct(new ProductSupply(bread, breadCount, breadWorth));
            supply.AddProduct(new ProductSupply(milk, milkCount, milkWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(milk, productsToBuy));
            purchase.AddProductPurchase(new ProductPurchase(bread, productsToBuy));
            shop.BuyProducts(purchase);
        }

        [Test]
        public void ChangeProductWorth_WorthHasBeenChanged()
        {
            const int initialWorth = 100;
            const int finiteWorth = 200;
            
            Shop shop = _shopManager.CreateShop("shop", new Address("address"));
            Product milk = shop.RegisterProduct("milk");
            milk.Worth = initialWorth;
            Assert.AreEqual(initialWorth, shop.GetProduct("milk").Worth);
            shop.GetProduct("milk").Worth = finiteWorth;
            Assert.AreEqual(milk.Worth, finiteWorth);

        }

        [Test]
        public void FindMostProfitableShop_FoundCorrectShop()
        {
            const string productName = "product";
            const int productToBuy = 10;
            const int productQuantity = 10;
            const int customerBalance = 1000;

            var shops = new List<Shop>();

            foreach (int num in Enumerable.Range(1, 5))
            {
                shops.Add(_shopManager.CreateShop($"shop_{num}", new Address($"address_{num}")));
                Product product = shops[num - 1].RegisterProduct(productName);
                var supply = new Supply();
                supply.AddProduct(new ProductSupply(product, productQuantity, num));
                shops[num - 1].MakeSupply(supply);
            }

            Shop mostProfitableShop = shops[0];
            var purchase = new Purchase(new Customer("Misha", customerBalance));
            purchase.AddProductPurchase(new ProductPurchase(new Product(productName), productToBuy));

            Assert.AreEqual(mostProfitableShop, _shopManager.GetMostProfitableShop(purchase));
        }

        [Test]
        public void FindMostProfitableShop_ShopWithEnoughProductsNotExists_ExceptionThrown()
        {
            const string productName = "product";
            const int productToBuy = 10;
            const int productQuantity = 9;
            const int customerBalance = 1000;

            foreach (int num in Enumerable.Range(1, 5))
            {
                Shop shop = _shopManager.CreateShop($"shop_{num}", new Address($"address_{num}"));
                Product product = shop.RegisterProduct(productName);
                var supply = new Supply();
                supply.AddProduct(new ProductSupply(product, productQuantity, num));
                shop.MakeSupply(supply);
            }

            var purchase = new Purchase(new Customer("Misha", customerBalance));
            purchase.AddProductPurchase(new ProductPurchase(new Product(productName), productToBuy));

            Assert.Catch<ShopManagerException>(() =>
            {
                _shopManager.GetMostProfitableShop(purchase);
            });
        }

        [Test]
        public void BuyProducts_MoneyTransferredAndProductQuantityDecreased()
        {
            const int initialQuantity = 10;
            const int finiteQuantity = 5;
            const int productWorth = 100;
            const int productsToBuy = 5;
            const int initialBalance = 1000;
            const int finiteBalance = 500;
            
            Shop shop = _shopManager.CreateShop("shop", new Address("address"));
            Product product = _shopManager.RegisterProduct("product");

            var supply = new Supply();
            supply.AddProduct(new ProductSupply(product, initialQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", initialBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(product, productsToBuy));
            shop.BuyProducts(purchase);
            
            Assert.AreEqual(finiteQuantity, product.Quantity);
            Assert.AreEqual(finiteBalance, customer.Balance);
        }

        [Test]
        public void BuyProducts_NotEnoughMoney_ExceptionThrown()
        {
            const int productQuantity = 10;
            const int productWorth = 100;
            const int productsToBuy = 5;
            const int customerBalance = 400;

            Shop shop = _shopManager.CreateShop("shop", new Address("address"));
            Product product = _shopManager.RegisterProduct("product");

            var supply = new Supply();
            supply.AddProduct(new ProductSupply(product, productQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(product, productsToBuy));

            Assert.Catch<ShopManagerException>(() =>
            {
                shop.BuyProducts(purchase);             
            });
        }

        [Test]
        public void BuyProducts_NotEnoughProducts_ExceptionThrown()
        {
            const int productQuantity = 5;
            const int productWorth = 100;
            const int productsToBuy = 9;
            const int customerBalance = 1000;

            Shop shop = _shopManager.CreateShop("shop", new Address("address"));
            Product product = _shopManager.RegisterProduct("product");

            var supply = new Supply();
            supply.AddProduct(new ProductSupply(product, productQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(product, productsToBuy));

            Assert.Catch<ShopManagerException>(() =>
            {
                shop.BuyProducts(purchase);             
            });
        }
    }
}