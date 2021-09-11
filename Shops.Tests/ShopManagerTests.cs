using NUnit.Framework;
using Shops;

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
            
        }

        [Test]
        public void ChangeProductWorth_WorthHasBeenChanged()
        {
            
        }
        
    }
}