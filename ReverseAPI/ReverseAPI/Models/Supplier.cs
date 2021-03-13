using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
