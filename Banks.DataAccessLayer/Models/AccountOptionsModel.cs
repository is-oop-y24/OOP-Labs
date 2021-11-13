namespace Banks.DataAccessLayer.Models
{
    public class AccountOptionsModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal DebitPercent { get; set; }
        public string DepositIntervals { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal CreditCommission { get; set; }
    }
}