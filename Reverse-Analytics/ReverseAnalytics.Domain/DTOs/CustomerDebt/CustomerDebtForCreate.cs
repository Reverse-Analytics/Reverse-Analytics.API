namespace ReverseAnalytics.Domain.DTOs.CustomerDebt
{
    public class CustomerDebtForCreate
    {
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }
        
        public int CustomerId { get; set; }
    }
}
