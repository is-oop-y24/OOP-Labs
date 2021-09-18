using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class ShopManager : IShopManager
    {
        private List<Shop> _shops = new List<Shop>();
        private List<Product> _products = new List<Product>();
        private int _currentShopId = 1;
        private int _curentproductId = 1;
        public ShopManager() { }

        public ReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public Shop CreateShop(string shopName, Address shopAddress)
        {
            var shop = new Shop(shopName, _currentShopId++);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string productName)
        {
            if (_products.Exists(product => product.Name == productName))
                throw new ShopManagerException($"The product {productName} is already registered.");

            var productId = new ProductId(_curentproductId++);
            var product = new Product(productName, productId);
            _products.Add(product);
            _shops.ForEach(shop => shop.RegisterProduct(productName, productId));
            return product;
        }

        // Returns the Shop list because shopName is not unique.
        public List<Shop> FindShop(string shopName)
        {
            return _shops
                .Where(shop => shop.Name == shopName)
                .Select(shop => shop)
                .ToList();
        }

        public Shop GetShop(ShopId shopId)
        {
            return _shops
                .Where(shop => shop.Id == shopId)
                .Select(shop => shop)
                .Single();
        }

        public Shop GetMostProfitableShop(Purchase purchase)
        {
            Shop shop = _shops
                .Where(shop => shop.AreProductsEnough(purchase))
                .OrderBy(shop => shop.CalculateTotalCost(purchase))
                .Select(shop => shop)
                .FirstOrDefault();

            if (shop == null)
                throw new ShopManagerException("There is not shop with enough products.");

            return shop;
        }
    }
}