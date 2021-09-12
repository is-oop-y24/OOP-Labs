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
            Assert.Fail();
        }

        [Test]
        public void ChangeProductWorth_WorthHasBeenChanged()
        {
            Assert.Fail();
        }

        [Test]
        public void FindMostProfitableShop_FoundCorrectShop()
        {
            Assert.Fail();
        }

        [Test]
        public void FindMostProfitableShop_ShopWithEnoughProductsNotExists_ExceptionThrown()
        {
            Assert.Catch<ShopManagerException>(() =>
            {
                
            });
        }

        [Test]
        public void BuyProducts_MoneyTransferredAndProductQuantitiesDecreased()
        {
            Assert.Fail();
        }

        [Test]
        public void BuyProducts_NotEnoughMoney_ExceptionThrown()
        {
            Assert.Catch<ShopManagerException>(() =>
            {
                
            });
        }

        [Test]
        public void BuyProducts_NotEnoughProducts_ExceptionThrown()
        {
            Assert.Catch<ShopManagerException>(() =>
            {
                
            });
        }
    }
}