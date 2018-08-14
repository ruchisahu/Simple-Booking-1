using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;
using WebMvcClient.Services;
using WebMvc.Infrastructure;
using WebMvc;

namespace WebMvcClient.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _apiClient;
        private readonly string _remoteServiceBaseUrl;
        public CatalogService(IOptionsSnapshot<AppSettings> settings,
           IHttpClient httpClient)
        {
            _settings = settings;
            _apiClient = httpClient;
            _remoteServiceBaseUrl = $"{_settings.Value.CatalogUrl}/api/catalog/";
        }
        public async Task<IEnumerable<SelectListItem>> GetCatagory()
        {
            var getCatagoryUri = ApiPaths.Catalog.GetCatalogItem(_remoteServiceBaseUrl);

            var dataString = await _apiClient.GetStringAsync(getCatagoryUri);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            var category = JArray.Parse(dataString);

            foreach (var cat in category.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = cat.Value<string>("id"),
                    Text = cat.Value<string>("Type")
                });
            }

            return items;
        }

        public async Task<Catalog> GetCatalogItems(int page, int take, int? EventCategory, int? Place)
        {
            var allcatalogItemsUri = ApiPaths.Catalog.GetAllCatalogItems(_remoteServiceBaseUrl, page, take, EventCategory, Place);

            var dataString = await _apiClient.GetStringAsync(allcatalogItemsUri);

            var response = JsonConvert.DeserializeObject<Catalog>(dataString);

            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetPlace()
        {
            var getCatagoryUri = ApiPaths.Catalog.GetCatalogItem(_remoteServiceBaseUrl);

            var dataString = await _apiClient.GetStringAsync(getCatagoryUri);

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            var category = JArray.Parse(dataString);

            foreach (var cat in category.Children<JObject>())
            {
                items.Add(new SelectListItem()
                {
                    Value = cat.Value<string>("id"),
                      Text = cat.Value<string>("Name")
                });
            }

            return items;
        }
    }
}