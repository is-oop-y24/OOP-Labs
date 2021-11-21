using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banks
{
    public class ClientIdentifier
    {
        public ClientIdentifier()
        {
        }
        
        public int Id { get; private init; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public int ClientId { get; private init; }
        public Client Client { get; private init; }
        
        [NotMapped]
        public bool IsIdentified => Passport != null && Address != null;
    }
}