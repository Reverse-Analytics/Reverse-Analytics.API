using System.ComponentModel.DataAnnotations;

namespace ReverseAPI.Models
{
    public class Supplier
    {
        [Key]
        public int IdSupplier { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
