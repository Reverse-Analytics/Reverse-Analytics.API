using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double Discount { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
