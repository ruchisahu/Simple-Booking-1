﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

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

            var response = await apiClient.PostAsync(remoteServiceBaseUrl, cart);
            response.EnsureSuccessStatusCode();
        }

        //Add the user ApplicationUser user
        public async Task<Cart> GetCart(string buyerId)
        {
            string basketURI = ApiPaths.Basket.GetBasket(remoteServiceBaseUrl, buyerId);
            var data = await apiClient.GetStringAsync(basketURI);
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
    }
}
