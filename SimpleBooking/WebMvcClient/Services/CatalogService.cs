using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;

namespace WebMvcClient.Services
{
    public class CatalogService
    {
        private readonly IHttpClient apiClient = new CustomHttpClient(null);

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
    }
}
