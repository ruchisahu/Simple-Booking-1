using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Polly.CircuitBreaker;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.Controllers
{
    public class CartListController : Controller
    {
        private readonly ICartService cartSvc;
        private IEventManagementService eventManagementService;
        private readonly string imageServiceBaseUrl;

        public CartListController(IOptionsSnapshot<AppSettings> settings, ICartService cartSvc, IEventManagementService evManagementService)
        {
            this.cartSvc = cartSvc;
            this.eventManagementService = evManagementService;
            this.imageServiceBaseUrl = $"{settings.Value.ImageUrl}/api/image";
        }

        public async Task<IActionResult> Index(string action)
        {
            if(action == "[ Checkout ]")
            {
                return RedirectToAction("Index", "order");
            }
     
            var vm = new CartListModel
            {
                EventsInCart = new List<Event>()
            };

            try
            {
                var cart = await cartSvc.GetCart("testUser");
                if (cart.Items != null)
                {
                    foreach (var cartItem in cart.Items)
                    {
                        var catalogEvent = await eventManagementService.GetEvent(cartItem.EventId);
                        catalogEvent.TicketsInCart = cartItem.TicketsPurchased;
                        catalogEvent.ImageURL = ApiPaths.Image.GetImageUrl(imageServiceBaseUrl, catalogEvent.ImageURL);
                        catalogEvent.DisplayPrice = catalogEvent.PriceType == EventPriceType.Free ? "Free" : ("$ " + catalogEvent.Price.ToString());
                        vm.EventsInCart.Add(catalogEvent);
                        vm.Total += cartItem.TicketsPurchased * catalogEvent.Price;
                    }
                }
            }
            catch (BrokenCircuitException)
            {
                // Catch error when CartApi is in open circuit mode
                ViewBag.IsBasketInoperative = true;
            }

            return View(vm);
        }
    }
}
