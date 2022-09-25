namespace ReverseAnalytics.Domain.DTOs.CustomerDebt
{
    public class CustomerDebtForUpdate
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
