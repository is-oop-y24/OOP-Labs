namespace Backups
{
    public interface IStorage
    {
        string StoragePath { get; }
        void Process();
    }
}