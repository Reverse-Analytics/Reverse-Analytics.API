using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReverseAPI.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CardPayment { get; set; }
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

    }
}
