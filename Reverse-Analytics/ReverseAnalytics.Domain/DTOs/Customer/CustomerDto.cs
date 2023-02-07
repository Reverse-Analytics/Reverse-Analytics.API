using ReverseAnalytics.Domain.DTOs.Debt;

namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string PrimaryPhone { get; set; }

        public ICollection<DebtDto> Debts { get; set; }

        public CustomerDto()
        {
            FullName = "";
        }
    }
}
