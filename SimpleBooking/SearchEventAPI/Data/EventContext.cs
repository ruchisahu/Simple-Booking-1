using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SearchEventAPI.Domain;

namespace SearchEventAPI.Data
{
    public class EventContext
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<Event>> GetEvents()
        {
            client.BaseAddress = new Uri("http://localhost:60672/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("api/Events/0");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
            }

            return GetMockedEvents();
        }

        public static List<Event> GetMockedEvents()
        {
            return new List<Event>()
            {
                 new Event()
            {
                ID = 1, Name= "Camping", Description ="Sleeping in a tent outdoor", Location ="North Bend",
                Type =EventType.Gathering, PriceType = EventPriceType.Free, Category= EventCategory.Travel, EventDate =DateTime.Now.Date,
                Price =0, EventImage = "image1"
            },

             new Event()
             {
                 ID = 2, Name= "Java Class", Description ="Learn Java", Location ="Bellevue",
                 Type =EventType.Class, PriceType = EventPriceType.Paid, Category= EventCategory.Tech, EventDate =DateTime.Now.Date,
                 Price =550, EventImage = "image2"
             },

              new Event()
               {
                 ID = 3, Name= "How to live well", Description ="Seminar about life", Location ="Bellevue",
                 Type =EventType.Seminar, PriceType = EventPriceType.Free, Category= EventCategory.Health, EventDate =DateTime.Now.Date,
                 Price =0, EventImage = "image3"
                }
            };
        }
    }
}
