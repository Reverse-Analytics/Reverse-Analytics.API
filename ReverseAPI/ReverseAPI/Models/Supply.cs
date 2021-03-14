using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReverseAPI.Models
{
    public class Supply
    {
        [Key]
        public int SupplyId { get; set; }
        public DateTime SupplyDate { get; set; }
        public decimal Payment { get; set; }
        public decimal Income { get; set; }
        public decimal TotalIncome { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
