using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebMvcClient.Models;

namespace WebMvcClient.ViewModels
{
    public class CatalogIndexViewModel
    {
        public CatalogIndexViewModel()
        {
            Location = "Bellevue";
        }

        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public string EventType { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        public string Price { get; set; }
        public IEnumerable<SelectListItem> Prices { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public string Location { get; set; }

        public PaginationInfo PaginationInfo { get; set; }
    }
}
