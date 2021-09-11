using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops
{
    public class Supply
    {
        private readonly List<ProductSupply> _products = new List<ProductSupply>();

        public Supply() { }

        public ReadOnlyCollection<ProductSupply> ProductSupplies => _products.AsReadOnly();
        
        public void AddProduct(ProductSupply productSupply)
        {
            _products.Add(productSupply);
        }
        
    }
}