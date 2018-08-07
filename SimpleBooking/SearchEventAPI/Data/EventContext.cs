using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchEventAPI.Domain;

namespace SearchEventAPI.Data
{
    public class EventContext
    {
        public static List<Event> GetEvents()
        {
            return new List<Event>()
            {
                 new Event()
            {
                ID = 1, Name= "Camping", Description ="Sleeping in a tent outdoor", Location ="North Bend",
                Type =EventType.Gathering, PriceType = EventPriceType.Free, Category= EventCategory.Travel, EventDate =DateTime.Now,
                Price =0, EventImage = "image1"
            },

             new Event()
             {
                 ID = 2, Name= "Java Class", Description ="Learn Java", Location ="Bellevue",
                 Type =EventType.Class, PriceType = EventPriceType.Paid, Category= EventCategory.Tech, EventDate =DateTime.Now,
                 Price =550, EventImage = "image2"
             },

              new Event()
               {
                 ID = 3, Name= "How to live well", Description ="Seminar about life", Location ="Bellevue",
                 Type =EventType.Seminar, PriceType = EventPriceType.Free, Category= EventCategory.Health, EventDate =DateTime.Now,
                 Price =0, EventImage = "image3"
                }
            };
        }
    }
}
