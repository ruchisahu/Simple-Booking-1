using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvcClient.Models
{
    public class Ticket
    {
        //Event Details
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventLocation { get; set; }

        //User Details
        public string UserName { get; set; }

        //Ticket Details
        public int TicketId { get; set; }
        public TicketStatus Status { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string AuthCode { get; set; }
        public DateTime ProcessingTime { get; set; }

    }
}
