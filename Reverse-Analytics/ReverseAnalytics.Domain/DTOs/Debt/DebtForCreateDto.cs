namespace ReverseAnalytics.Domain.DTOs.Debt
{
    public class DebtForCreateDto
    {
        public decimal Amount { get; set; }
        public DateTime DebtDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public DateTime DueDate { get; set; }

        public int PersonId { get; set; }
    }
}
