namespace Shops
{
    public class ProductPurchase
    {

        public ProductPurchase(Product product, int quantity)
        {
            ProductName = product.Name;
            Quantity = quantity;
        }
        
            
        public Customer Customer { get; }
        public string ProductName { get; }
        public int Quantity { get; }
    }
}