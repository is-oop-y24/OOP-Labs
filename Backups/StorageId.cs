namespace Backups
{
    public class StorageId
    {
        private int _id;

        public StorageId(int id)
        {
            _id = id;
        }

        public string GetStringId()
        {
            return _id.ToString();
        }
    }
}