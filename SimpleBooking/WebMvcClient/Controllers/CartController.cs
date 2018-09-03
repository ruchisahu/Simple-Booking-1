using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Models;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int eventId)
        {
            var catalogService = new CatalogService();

            var catalogEvent = await catalogService.GetEvent(eventId);

            var viewModel = new CartViewModel
            {
                Items = new CartEvent
                {
                    Items = new List<CartEventItem>
                    {
                        new CartEventItem
                        {
                            Event = catalogEvent
                        }
                    }
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<int, int> tickets, string action)
        {
            var cartService = new CartService();

            await cartService.Checkout("test", tickets);

            return View();
        }
    }
}
