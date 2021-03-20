using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
        public int Volume { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CardPayment { get; set; }
        public DateTime SaleDate { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
    }
}
