using System;
using System.Collections.Generic;

namespace EventManagementAPI.Domain
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EventPriceType PriceType { get; set; }
        public EventCategory Category { get; set; }
        public DateTime Date { get; set; }
        public Decimal Price { get; set; }
        public string ImageURL { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public int TicketId { get; set; }
        public virtual IEnumerable<Ticket> Tickets { get; set; }
    }
}
