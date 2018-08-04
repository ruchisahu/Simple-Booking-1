using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Domain
{
    
    public class Ticket
    {
        public Ticket()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }

        public TicketStatus Status { get; set; }

        public int Quantity { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
