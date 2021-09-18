namespace Shops
{
    public class ProductId
    {
        private int _id;

        public ProductId(int productId)
        {
            _id = productId;
        }

        public int GetId()
        {
            return _id;
        }
    }
}