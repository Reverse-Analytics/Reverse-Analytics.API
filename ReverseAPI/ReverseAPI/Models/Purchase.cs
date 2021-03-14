using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReverseAPI.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
    }
}
