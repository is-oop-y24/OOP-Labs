namespace Shops
{
    public class ProductId
    {
        private static int _currentId = 1;
        private int _id;

        public ProductId(int productId)
        {
            _id = productId;
        }

        public static ProductId NewId()
        {
            return new ProductId(_currentId++);
        }

        public int GetId()
        {
            return _id;
        }
    }
}