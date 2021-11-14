namespace Banks
{
    public class ClientId
    {
        public string Passport { get; init; }
        public string Address { get; init; }

        public bool IsIdentified => Passport != null && Address != null;
    }
}