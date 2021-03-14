using System.ComponentModel.DataAnnotations;

namespace ReverseAPI.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

    }
}
