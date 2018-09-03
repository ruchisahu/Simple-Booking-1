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
        private IEventManagementService eventManagementService;

        private ICartService cartService;

        public CartController(IEventManagementService eventManagementService, ICartService cartService)
        {
            this.eventManagementService = eventManagementService;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int eventId)
        {
            var catalogEvent = await eventManagementService.GetEvent(eventId);

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
            await cartService.Checkout("test", tickets);
            return RedirectToAction("Index", "order");
        }
    }
}
