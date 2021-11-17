using System.ComponentModel.DataAnnotations.Schema;

namespace Banks
{
    public class ClientId
    {
        public int Id { get; private init; }
        public string Passport { get; private init; }
        public string Address { get; private init; }

        [NotMapped]
        public bool IsIdentified => Passport != null && Address != null;
    }
}