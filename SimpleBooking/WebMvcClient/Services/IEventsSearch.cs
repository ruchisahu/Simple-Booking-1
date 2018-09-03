using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public interface IEventsSearch
    {
        Task<List<Event>> Events(string location, string eventType, string eventCategory, string priceType);

        Task<IEnumerable<SelectListItem>> Categories();

        Task<IEnumerable<SelectListItem>> Types();

        Task<IEnumerable<SelectListItem>> PriceType();
    }
}
