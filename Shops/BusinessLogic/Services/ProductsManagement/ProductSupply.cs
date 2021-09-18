using Shops.Tools;

namespace Shops
{
    public class ProductSupply
    {
        private int _quantity;
        private int _worth;

        public ProductSupply(ProductId productId, int quantity, int worth)
        {
            ProductId = productId;
            Quantity = quantity;
            Worth = worth;
        }

        public int Worth
        {
            get => _worth;
            private init
            {
                if (value < 0)
                    throw new ShopManagerException("Product worth cannot be a negative number");
                _worth = value;
            }
        }

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

        public string ProductName { get; }
        public ProductId ProductId { get; }
    }
}