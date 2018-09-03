using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public class EventsSearchService : IEventsSearch
    {
        private readonly IOptionsSnapshot<AppSettings> settings;
        private readonly IHttpClient apiClient;
        private readonly string remoteServiceBaseUrl;
        private readonly string imageServiceBaseUrl;

        public EventsSearchService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient)
        {
            this.settings = settings;
            this.apiClient = httpClient;
            this.remoteServiceBaseUrl = $"{settings.Value.EventsSearchUrl}/api/EventsSearch";
            this.imageServiceBaseUrl = $"{settings.Value.ImageUrl}/api/image";
        }

        public async Task<List<Event>> Events(string location, string eventType, string eventCategory, string priceType)
        {
            var getEventsUri = ApiPaths.EventsSearch.GetEvents(remoteServiceBaseUrl, location, eventType, eventCategory, priceType);
            var data = await apiClient.GetStringAsync(getEventsUri);
            var events = JsonConvert.DeserializeObject<Catalog>(data);
            foreach (var eventData in events.Data)
            {
                eventData.ImageURL = ApiPaths.Image.GetImageUrl(imageServiceBaseUrl, eventData.ImageURL);
            }
            return events.Data;
        }

        public async Task<IEnumerable<SelectListItem>> Categories()
        {
            var getCategoriesUri = ApiPaths.EventsSearch.GetCategories(remoteServiceBaseUrl);
            var data = await apiClient.GetStringAsync(getCategoriesUri);
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
            var getTypesUri = ApiPaths.EventsSearch.GetTypes(remoteServiceBaseUrl);
            var data = await apiClient.GetStringAsync(getTypesUri);
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
            var getGetPricesUri = ApiPaths.EventsSearch.GetPrices(remoteServiceBaseUrl);
            var data = await apiClient.GetStringAsync(getGetPricesUri);
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
    }
}
