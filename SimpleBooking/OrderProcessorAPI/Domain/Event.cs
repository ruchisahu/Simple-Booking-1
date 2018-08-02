using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Domain
{
    public class Event
    {
        public Event()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public Decimal Price { get; set; }

        public DateTime EventDate { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
