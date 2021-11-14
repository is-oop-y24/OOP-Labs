using Banks.DataAccessLayer.Models;

namespace Banks
{
    public class Client
    {
        public Client(string name)
        {
            Name = name;
        }

        public int Id { get; init; }
        public string Name { get; init; }
        public ClientId Identifier { get; set; }
        public bool IsDoubtful => (Identifier != null) && Identifier.IsIdentified;

        public void Notify()
        {
            // Some notify logic
        }
    }
}