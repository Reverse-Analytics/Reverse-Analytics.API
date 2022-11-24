using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.DTOs.Debt;
using ReverseAnalytics.Domain.DTOs.Phone;

namespace ReverseAnalytics.Domain.DTOs.CustomerPhoneDto
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? CompanyName { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonPhone { get; set; }
        public bool IsActive { get; set; }

        public ICollection<AddressDto> Addresses { get; set; }
        public ICollection<DebtDto> Debts { get; set; }
        public ICollection<PhoneDto> Phones { get; set; }

        public CustomerDto()
        {
            FullName = "";
        }
    }
}
