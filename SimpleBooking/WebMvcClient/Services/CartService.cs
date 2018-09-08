using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace WebMvcClient.Services
{
    public class CartService : ICartService
    {
        private readonly IOptionsSnapshot<AppSettings> settings;
        private readonly IHttpClient apiClient;
        private readonly IHttpContextAccessor httpContextAccesor;
        private readonly string remoteServiceBaseUrl;

        public CartService(IOptionsSnapshot<AppSettings> settings, IHttpContextAccessor httpContextAccesor, IHttpClient httpClient)
        {
            this.settings = settings;
            this.apiClient = httpClient;
            this.httpContextAccesor = httpContextAccesor;
            this.remoteServiceBaseUrl = $"{settings.Value.CartUrl}/api/Tickets";
        }

        public async Task Checkout(string buyerId, Dictionary<int, int> tickets)
        {
            var token = await GetUserTokenAsync();
            var cart = await GetCart(buyerId);

            if (cart == null)
            {
                cart = new Cart
                {
                    BuyerId = buyerId,
                    Items = new List<CartTicket>()
                };
            }

            foreach (var ticket in tickets)
            {
                var existingItem = cart.Items.Where(p => p.EventId == ticket.Key).FirstOrDefault();

                if (existingItem == null)
                {
                    existingItem = new CartTicket
                    {
                        EventId = ticket.Key
                    };
                    cart.Items.Add(existingItem);
                }

                existingItem.TicketsPurchased += ticket.Value;
            }

            var response = await apiClient.PostAsync(remoteServiceBaseUrl, cart, token);
            response.EnsureSuccessStatusCode();
        }

        //Add the user ApplicationUser user
        public async Task<Cart> GetCart(string buyerId)
        {
            var token = await GetUserTokenAsync();
            string basketURI = ApiPaths.Basket.GetBasket(remoteServiceBaseUrl, buyerId);
            var data = await apiClient.GetStringAsync(basketURI, token);
            var response = JsonConvert.DeserializeObject<Cart>(data.ToString());
            if (response == null)
            {
                return response = new Cart
                {
                    BuyerId = buyerId,
                    Items = new List<CartTicket>()
                };
            }
            return response;
        }

        async Task<string> GetUserTokenAsync()
        {
            var context = httpContextAccesor.HttpContext;

            return await context.GetTokenAsync("access_token");
        }
    }
}
