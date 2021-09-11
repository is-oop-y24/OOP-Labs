namespace Shops
{
    public class ProductPurchase
    {

        public ProductPurchase(Customer customer, Product product, int quantity)
        {
            Customer = customer;
            ProductName = product.Name;
            Quantity = quantity;
        }
        
            
        public Customer Customer { get; }
        public string ProductName { get; }
        public int Quantity { get; }
    }
}