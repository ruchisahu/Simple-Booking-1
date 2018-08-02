using OrderProcessorAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Response_Entities
{/// <summary>
/// These all details will get print on a ticket
/// </summary>
    public class TicketForJson
    {
        //Event Details
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventLocation { get; set; }

        //User Details
        public string UserName { get; set; }

        //Ticket Details
        public int TicketId { get; set; }
        public TicketStatus status { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string  AuthCode { get; set; }
        public DateTime ProcessingTime { get; set; }

    }
}
