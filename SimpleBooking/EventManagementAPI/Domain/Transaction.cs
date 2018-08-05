using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagementAPI.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ProcessingTime { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
