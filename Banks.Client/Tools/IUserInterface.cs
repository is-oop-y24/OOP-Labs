using Spectre.Console;

namespace Banks
{
    public interface IUserInterface
    {
        void WriteMessage(params object[] parameters);
        void ShowTable(Table table);
        string Read();
    }
}