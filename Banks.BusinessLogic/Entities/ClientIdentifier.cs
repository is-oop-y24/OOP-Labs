using System.ComponentModel.DataAnnotations.Schema;

namespace Banks
{
    public class ClientIdentifier
    {
        public ClientIdentifier()
        {
        }
        
        public int Id { get; private init; }
        public string Passport { get; init; }
        public string Address { get; init; }
        
        [NotMapped]
        public bool IsIdentified => Passport != null && Address != null;
    }
}