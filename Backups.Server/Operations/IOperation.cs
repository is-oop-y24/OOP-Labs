namespace Backups.Server
{
    public interface IOperation
    {
        Response Execute();
    }
}