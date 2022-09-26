using ReverseAnalytics.Domain.DTOs.CustomerDebt;
using ReverseAnalytics.Domain.DTOs.CustomerPhone;
using ReverseAnalytics.Domain.Entities;

namespace ReverseAnalytics.Domain.DTOs.CustomerPhoneDto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }

        public ICollection<CustomerPhone.CustomerPhoneDto> CustomerPhones { get; set; }
        public ICollection<CustomerDebtDto> CustomerDebts { get; set; }
        public ICollection<Order> Orders { get; set; }

        public CustomerDto()
        {
            FirstName = "";
            CustomerPhones = new List<CustomerPhone.CustomerPhoneDto>();
            CustomerDebts = new List<CustomerDebtDto>();
            Orders = new List<Order>();
        }
    }
}
