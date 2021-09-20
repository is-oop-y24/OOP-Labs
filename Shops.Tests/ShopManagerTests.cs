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
            supply.AddProduct(new ProductSupply(milk.Id, milkCount, milkWorth));
            supply.AddProduct(new ProductSupply(bread.Id, breadCount, breadWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(milk.Id, productsToBuy));
            purchase.AddProductPurchase(new ProductPurchase(bread.Id, productsToBuy));
            shop.BuyProducts(purchase);
        }

        [Test]
        public void ChangeProductWorth_WorthHasBeenChanged()
        {
            const int initialWorth = 100;
            const int finiteWorth = 200;
            
            Shop shop = _shopManager.CreateShop("shop", new Address("address"));
            Product milk = shop.RegisterProduct("milk", new ProductId(1));
            var supply = new Supply();
            supply.AddProduct(new ProductSupply(milk.Id, 1, initialWorth));
            shop.MakeSupply(supply);
            Assert.AreEqual(initialWorth, shop.GetProduct(milk.Id).Worth);
            var supply2 = new Supply();
            supply2.AddProduct(new ProductSupply(milk.Id, 1, finiteWorth));
            shop.MakeSupply(supply2);
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
                Product product = shops[num - 1].RegisterProduct(productName, new ProductId(1));
                var supply = new Supply();
                supply.AddProduct(new ProductSupply(new ProductId(1), productQuantity, num));
                shops[num - 1].MakeSupply(supply);
            }

            Shop mostProfitableShop = shops[0];
            var purchase = new Purchase(new Customer("Misha", customerBalance));
            purchase.AddProductPurchase(new ProductPurchase(new ProductId(1), productToBuy));

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
                Product product = shop.RegisterProduct(productName, new ProductId(1));
                var supply = new Supply();
                supply.AddProduct(new ProductSupply(new ProductId(1), productQuantity, num));
                shop.MakeSupply(supply);
            }

            var purchase = new Purchase(new Customer("Misha", customerBalance));
            purchase.AddProductPurchase(new ProductPurchase(new ProductId(1), productToBuy));

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
            supply.AddProduct(new ProductSupply(new ProductId(1), initialQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", initialBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(new ProductId(1), productsToBuy));
            shop.BuyProducts(purchase);
            
            Assert.AreEqual(finiteQuantity, shop.GetProduct(new ProductId(1)).Quantity);
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
            supply.AddProduct(new ProductSupply(product.Id, productQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(product.Id, productsToBuy));

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
            supply.AddProduct(new ProductSupply(product.Id, productQuantity, productWorth));
            shop.MakeSupply(supply);

            var customer = new Customer("Misha", customerBalance);
            var purchase = new Purchase(customer);
            purchase.AddProductPurchase(new ProductPurchase(product.Id, productsToBuy));

            Assert.Catch<ShopManagerException>(() =>
            {
                shop.BuyProducts(purchase);             
            });
        }
    }
}