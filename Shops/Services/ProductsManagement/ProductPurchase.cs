namespace Shops
{
    public class ProductPurchase
    {

        public ProductPurchase(Customer customer, Product product, int quantity)
        {
            Customer = customer;
            Product = product;
            Quantity = quantity;
        }
        
            
        public Customer Customer { get; }
        public Product Product { get; }
        public int Quantity { get; }
    }
}