using Shops.Tools;

namespace Shops
{
    public class ProductSupply
    {
        private int _quantity;

        public ProductSupply(ProductId productId, int quantity, int worth)
        {
            if (worth < 0)
                throw new ShopManagerException("Product worth cannot be a negative number");

            ProductId = productId;
            Quantity = quantity;
            Worth = worth;
        }

        public int Worth { get; }

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