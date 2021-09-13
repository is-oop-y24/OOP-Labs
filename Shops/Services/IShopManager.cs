using System.Collections.Generic;

namespace Shops
{
    public interface IShopManager
    {
        Shop CreateShop(string shopName, Address shopAddress);
        Product RegisterProduct(string productName);
        List<Shop> FindShop(string shopName);
        Shop GetShop(ShopId shopId);
        Shop GetMostProfitableShop(Purchase purchase);
    }
}