using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System.Threading.Tasks;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.ViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartService cartSvc;

        public Cart(ICartService cartSvc)
        {
            this.cartSvc = cartSvc;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new CartComponentViewModel();
            try
            {
                var cart = await cartSvc.GetCart("testUser");
                if (cart.Items == null)
                {
                    vm.ItemsInCart = 0;
                }
                else
                {
                    vm.ItemsInCart = cart.Items.Count;
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
