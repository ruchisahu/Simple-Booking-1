using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEventAPI.Domain
{
    public enum EventType { Class, Conference, Seminar, Tournament, Game, Festival, Gathering };
    public enum EventPriceType { Paid, Free };
    public enum EventCategory { Art, Business, Health, Tech, Travel, Sports, science };

    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public EventType Type { get; set; }
        public EventPriceType PriceType { get; set; }
        public EventCategory Category { get; set; }
        public DateTime EventDate { get; set; }
        public decimal Price { get; set; }
        public String EventImage { get; set; }
    }
}
