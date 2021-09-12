using Shops.Tools;

namespace Shops
{
    public class ProductPurchase
    {

        private int _quantity;
        
        public ProductPurchase(Product product, int quantity)
        {
            ProductName = product.Name;
            Quantity = quantity;
        }

        public Customer Customer { get; }
        public string ProductName { get; }

        public int Quantity
        {
            get => _quantity;
            private init
            {
                if (value < 0)
                    throw new ShopManagerException("Product quantity cannot be a negative number");
                _quantity = value;
            }
            
    }
    }
}