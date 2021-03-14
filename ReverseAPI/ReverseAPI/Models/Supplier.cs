using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReverseAPI.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Supply> Supplies { get; set; }
    }
}
