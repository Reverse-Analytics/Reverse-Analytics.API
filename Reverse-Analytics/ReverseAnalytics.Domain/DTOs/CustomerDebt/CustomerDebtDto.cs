namespace ReverseAnalytics.Domain.DTOs.CustomerDebt
{
    public class CustomerDebtDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime DueDate { get; set; }

        public int CustomerId { get; set; }
    }
}
