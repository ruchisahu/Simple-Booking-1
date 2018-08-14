using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebMvcClient.ViewModels
{
    public class CatalogIndexViewModel
    {
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
