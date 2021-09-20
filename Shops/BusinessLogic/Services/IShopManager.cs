using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops
{
    public interface IShopManager
    {
        ReadOnlyCollection<Product> Products { get; }
        ReadOnlyCollection<Shop> Shops { get; }

        Shop CreateShop(string shopName, Address shopAddress);
        Product RegisterProduct(string productName);
        List<Shop> FindShop(string shopName);
        Shop GetShop(ShopId shopId);
        Shop GetMostProfitableShop(Purchase purchase);
    }
}