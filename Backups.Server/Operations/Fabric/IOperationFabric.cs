namespace Backups.Server
{
    public interface IOperationFabric
    {
        IOperation GetOperation(Request request);
    }
}