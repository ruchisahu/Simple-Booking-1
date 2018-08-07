using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketsAPI.Domain
{
    public class Ticket
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int TicketsPurchased { get; set; }
    }
}
