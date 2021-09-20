using Spectre.Console;

namespace Shops
{
    public interface IUserInterface
    {
        void WriteLine(string message);
        void WriteError(string errorMessage);
        void ShowTable(Table table);
        string ReadLine();
    }
}