namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            new Application(new SystemLoader().Load()).Run();
        }
    }
}
