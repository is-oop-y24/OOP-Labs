namespace Banks
{
    public interface IUserInterface
    {
        void WriteMessage(params object[] parameters);
        string Read();
    }
}