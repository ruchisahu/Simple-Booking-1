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
        //Event Details
        public string EventName { get; set; }
        public string Location { get; set; }
        public Decimal Price { get; set; }
        public DateTime EventDate { get; set; }

        //No. Of Tickets
        public int Quantity { get; set; }

        //User Details
        public string UserName { get; set; }
        public string BillingAddress { get; set; }
        public string EmailAddress { get; set; }
        public long CreditCardNo { get; set; }
        public int CSC { get; set; }
        public string ExpirationDate { get; set; }
        
    }
}
