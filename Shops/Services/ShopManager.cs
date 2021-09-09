using System.Collections.Generic;

namespace Shops
{
    public class ShopManager : IShopManager
    {

        public ShopManager()
        {
            
        }

        public Shop CreateShop(string shopName, Address shopAddress)
        {
            throw new System.NotImplementedException();
        }

        public List<Shop> FindShop(string shopName)
        {
            throw new System.NotImplementedException();
        }

        public Shop GetShop(ShopId shopId)
        {
            throw new System.NotImplementedException();
        }

        public Shop GetMostProfitableShop(Purchase purchase)
        {
            throw new System.NotImplementedException();
        }
    }
}