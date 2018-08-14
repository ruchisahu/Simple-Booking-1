using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvc.ViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
        public IEnumerable<SelectListItem> EventType { get; set; }
        public IEnumerable<SelectListItem> Place { get; set; }
        public int? EventFilterApplied { get; set; }
        public int? PlaceFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
