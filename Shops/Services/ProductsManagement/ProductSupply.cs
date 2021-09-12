using Shops.Tools;

namespace Shops
{
    public class ProductSupply
    {
        public ProductSupply(Product product, int quantity, int worth)
        {
            if (quantity < 0)
                throw new ShopManagerException("Product quantity cannot be a negative number");
            if (worth < 0)
                throw new ShopManagerException("Product worth cannot be a negative number");

            ProductName = product.Name;
            Quantity = quantity;
            Worth = worth;
        }

        public int Worth { get; }
        public int Quantity { get; }
        public string ProductName { get; }
    }
}