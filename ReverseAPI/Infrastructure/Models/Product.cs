using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal IncomePrice { get; set; }
        public decimal SalePrice { get; set; }
        public double Volume { get; set; }
        public int Leftover { get; set; }

        public ICollection<Supply> Supplies { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
