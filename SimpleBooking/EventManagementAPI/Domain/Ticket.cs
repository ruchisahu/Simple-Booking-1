using System.Collections.Generic;

namespace EventManagementAPI.Domain
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public int TransactionId { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }
    }
}