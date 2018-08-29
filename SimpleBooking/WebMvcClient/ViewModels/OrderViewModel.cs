using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcClient.Models
{
    public class OrderViewModel
    {
        public string BuyerId { get; set; }

        [DisplayName("Event name")]
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public Decimal Price { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string AuthCode { get; set; }

        public string UserName { get; set; }
        public string BillingAddress { get; set; }
        public string EmailAddress { get; set; }
        public string StripeToken { get; set;}
    }
}
