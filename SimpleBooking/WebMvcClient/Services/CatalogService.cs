using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public class CatalogService
    {
        private readonly IHttpClient apiClient = new CustomHttpClient(null);

        public async Task<List<Event>> Events(string location, string eventType, string eventCategory, string priceType)
        {
            var apiUrl = $"http://localhost:49572/api/EventsSearch/Events?location={location}&anyText={location}&eventType={eventType}&eventCategory={eventCategory}&priceType={priceType}";
            var data = await apiClient.GetStringAsync(apiUrl);
            var events = JsonConvert.DeserializeObject<Catalog>(data);
            return events.Data;
        }

        public async Task<IEnumerable<SelectListItem>> Categories()
        {
            var apiUrl = "http://localhost:49572/api/EventsSearch/Categories";

            var data = await apiClient.GetStringAsync(apiUrl);

            var categories = JArray.Parse(data);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All Events", Selected = true }
            };

            foreach (var category in categories)
            {
                items.Add(new SelectListItem()
                {
                    Value = (string)category,
                    Text = (string)category
                });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> Types()
        {
            var apiUrl = "http://localhost:49572/api/EventsSearch/Types";

            var data = await apiClient.GetStringAsync(apiUrl);

            var eventTypes = JArray.Parse(data);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "AllTypes", Selected = true }
            };

            foreach (var type in eventTypes)
            {
                items.Add(new SelectListItem()
                {
                    Value = (string)type,
                    Text = (string)type,
                });
            }
            return items;
        }

        public async Task<IEnumerable<SelectListItem>> PriceType()
        {
            var apiUrl = "http://localhost:49572/api/EventsSearch/Prices";

            var data = await apiClient.GetStringAsync(apiUrl);

            var priceTypes = JArray.Parse(data);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All Prices", Selected = true }
            };

            foreach (var price in priceTypes)
            {
                items.Add(new SelectListItem()
                {
                    Value = (string)price,
                    Text = (string)price,
                });
            }
            return items;
        }

        public async Task<Event> GetEvent(int eventId)
        {
            var apiUrl = $"http://localhost:49572/api/EventManagement/{eventId}";

            var data = await apiClient.GetStringAsync(apiUrl);

            var eventData = JsonConvert.DeserializeObject<Event>(data);
            return eventData;
        }
    }
}
