using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcClient.Models
{
    public class OrderViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public decimal EventPrice { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public string UserFirstname { get; set; }
        public string UserLastName { get; set; }
        public string TelephoneNo { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }
        public string Address1 { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }

    }
}
