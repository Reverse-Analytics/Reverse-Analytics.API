namespace ReverseAnalytics.Domain.DTOs.Debt
{
    public class DebtDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        public int PersonId { get; set; }
    }
}
