using Shops.Tools;

namespace Shops
{
    public class ProductPurchase
    {
        private int _quantity;

        public ProductPurchase(ProductId productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public ProductId ProductId { get; }

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