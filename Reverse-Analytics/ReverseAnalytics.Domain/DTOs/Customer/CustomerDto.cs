using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Sale;

namespace ReverseAnalytics.Domain.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<DebtDto> Debts { get; set; }
        public virtual ICollection<SaleDto> Sales { get; set; }

        public CustomerDto()
        {
            FullName = "";
        }
    }
}
