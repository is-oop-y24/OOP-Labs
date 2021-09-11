using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shops
{
    public class Purchase
    {
        private readonly List<ProductPurchase> _productPurchases = new List<ProductPurchase>();

        public Purchase()
        {
            
        }

        public ReadOnlyCollection<ProductPurchase> ProductPurchases => _productPurchases.AsReadOnly();


        public void AddProductPurchase(ProductPurchase productPurchase)
        {
            _productPurchases.Add(productPurchase);
        }
    }
}