using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Models;

namespace WebMvcClient.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetCatalogItems(int page, int take, int? EventCategory, int? Place);
        Task<IEnumerable<SelectListItem>> GetCatagory();
        Task<IEnumerable<SelectListItem>> GetPlace();
    }
}
