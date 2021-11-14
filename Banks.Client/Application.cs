using System.Text;

namespace Banks
{
    public class Application
    {
        private readonly ICentralBank _centralBank;

        public Application(ICentralBank centralBank)
        {
            _centralBank = centralBank;
        }

        public void Run()
        {
        }
    }
}