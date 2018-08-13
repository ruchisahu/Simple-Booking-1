using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
       
        [ForeignKey("EventId")]
        public int EventId { get; set; }
        public EventCatalog Eventcatalog { get; set; }
    }
}