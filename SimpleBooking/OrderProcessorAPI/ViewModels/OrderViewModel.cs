using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.ViewModels
{
    /// <summary>
    /// These details we will get from UI for ticket generation
    /// </summary>
    public class OrderViewModel
    {
        public string BuyerId { get; set; }

        //Event Details
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public Decimal Price { get; set; }


        //No. Of Tickets
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string AuthCode { get; set; }

        //User Details
        public string UserName { get; set; }
        public string BillingAddress { get; set; }
        public string EmailAddress { get; set; }
    }
}
