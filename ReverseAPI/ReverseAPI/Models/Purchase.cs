using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReverseAPI.Models
{
    public class Purchase
    {
        [Key]
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TotalAmount { get; set; }
        public decimal Discount { get; set; }

        public int ClientId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
