namespace Backups.Server
{
    public interface IOperationFactory
    {
        IOperation GetOperation(Request request);
    }
}