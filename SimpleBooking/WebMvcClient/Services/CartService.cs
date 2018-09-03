using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public class CartService
    {
        private readonly IHttpClient apiClient = new CustomHttpClient(null);

        public async Task Checkout(string buyerId, Dictionary<int, int> tickets)
        {
            var apiUrl = $"http://localhost:5050/api/Tickets";

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

            var response = await apiClient.PostAsync(apiUrl, cart);
            response.EnsureSuccessStatusCode();
        }
    }
}
