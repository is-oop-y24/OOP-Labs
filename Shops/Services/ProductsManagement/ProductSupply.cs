namespace Shops
{
    public class ProductSupply
    {
        private int _quantity;
        private int _worth;
        public Product Product { get; }

        public ProductSupply(Product product, int quantity, int worth)
        {
            _quantity = quantity;
            _worth = worth;
            Product = product;
        }
    }
}