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
        
        
    }
}