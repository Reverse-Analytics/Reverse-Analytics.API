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
        public ICollection<CustomerDebt> CustomerDebts { get; set; }
        public ICollection<Order> Orders { get; set; }

        public CustomerDto()
        {
            FirstName = "";
            CustomerPhones = new List<CustomerPhone.CustomerPhoneDto>();
            CustomerDebts = new List<CustomerDebt>();
            Orders = new List<Order>();
        }
    }
}
