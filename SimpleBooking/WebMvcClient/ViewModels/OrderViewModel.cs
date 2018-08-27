using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcClient.Models
{
    public class OrderViewModel
    {
        public int EventId { get; set; }
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
        public long CreditCardNo { get; set; }
        public int CSC { get; set; }
        public string ExpirationDate { get; set; }

        //public string UserFirstname { get; set; }
        //public string UserLastName { get; set; }
        //public string TelephoneNo { get; set; }
        //public string CountryName { get; set; }
        //public string Address { get; set; }
        //public string Address1 { get; set; }
        //public string CityName { get; set; }
        //public string State { get; set; }
        //public int ZipCode { get; set; }

    }
}
