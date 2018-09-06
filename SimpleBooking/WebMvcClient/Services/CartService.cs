using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WebMvcClient.Services
{
    public class CartService : ICartService
    {
        private readonly IOptionsSnapshot<AppSettings> settings;
        private readonly IHttpClient apiClient;
        private readonly string remoteServiceBaseUrl;

        public CartService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient)
        {
            this.settings = settings;
            this.apiClient = httpClient;
            this.remoteServiceBaseUrl = $"{settings.Value.CartUrl}/api/Tickets";
        }

        public async Task Checkout(string buyerId, Dictionary<int, int> tickets)
        {
            var cart = new Cart
            {
                BuyerId = buyerId
            };

            cart.Items = new List<CartTicket>();
            foreach (var ticket in tickets)
            {
                var cartTicket = new CartTicket
                {
                    EventId = ticket.Key,
                    TicketsPurchased = ticket.Value
                };
                cart.Items.Add(cartTicket);
            }

            var response = await apiClient.PostAsync(remoteServiceBaseUrl, cart);
            response.EnsureSuccessStatusCode();
        }
        //Add the user ApplicationUser user
        public async Task<Cart> GetCart(string userID)
        {
            string basketURI = ApiPaths.Basket.GetBasket(remoteServiceBaseUrl, userID);
            var data = await apiClient.GetStringAsync(basketURI);
            var response = JsonConvert.DeserializeObject<Cart>(data.ToString());
            if(response==null)
            {
                return response = new Cart { BuyerId = userID };
            }
            return response;
        }
    }
}
