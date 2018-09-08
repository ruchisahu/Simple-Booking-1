using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Models;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IEventManagementService eventManagementService;

        private ICartService cartService;

        private readonly IIdentityService<ApplicationUser> identitySvc;

        public CartController(IEventManagementService eventManagementService, ICartService cartService, IIdentityService<ApplicationUser> identitySvc)
        {
            this.eventManagementService = eventManagementService;
            this.cartService = cartService;
            this.identitySvc = identitySvc;
        }

        //[HttpPost]
        public async Task<IActionResult> Index(int eventId)
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
        public async Task<IActionResult> Checkout(Dictionary<int, int> tickets, string action)
        {
            var user = identitySvc.Get(HttpContext.User);

            await cartService.Checkout(user.Email, tickets);
            return RedirectToAction("Index", "order");
        }
    }
}
