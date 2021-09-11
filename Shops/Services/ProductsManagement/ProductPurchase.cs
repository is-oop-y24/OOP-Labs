using Shops.Tools;

namespace Shops
{
    public class ProductPurchase
    {
        public ProductPurchase(Product product, int quantity)
        {
            if (quantity < 0)
                throw new ShopManagerException("Product quantity cannot be a negative number");
            
            ProductName = product.Name;
            Quantity = quantity;
        }

        public Customer Customer { get; }
        public string ProductName { get; }
        public int Quantity { get; }
    }
}