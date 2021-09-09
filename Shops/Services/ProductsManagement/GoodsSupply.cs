using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops
{
    public class GoodsSupply
    {
        private readonly List<ProductSupply> _products = new List<ProductSupply>();

        public GoodsSupply() { }

        public ReadOnlyCollection<ProductSupply> ProductSupplies => _products.AsReadOnly();

        public void AddProduct(Product product, int quantity, int worth)
        {
            _products.Add(new ProductSupply(product, quantity, worth));
        }

        public void AddProduct(ProductSupply productSupply)
        {
            _products.Add(productSupply);
        }
        
    }
}