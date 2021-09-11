namespace Shops
{
    public class ProductSupply
    {
        public ProductSupply(Product product, int quantity, int worth)
        {
            ProductName = product.Name;
            Quantity = quantity;
            Worth = worth;
        }

        public int Worth { get; }
        public int Quantity { get; }
        public string ProductName { get; }
    }
}