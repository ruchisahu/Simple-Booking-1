using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebMvcClient.Infrastructure;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly string _remoteServiceBaseUrl;
        public OrderService(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
            _remoteServiceBaseUrl= $"{_settings.Value.OrderUrl}/api/order";
            _client = new HttpClient();
        }


        public async Task<Ticket> ProcessAnOrder(OrderViewModel orderViewModel)
        {
            var processAnOrderUrl = ApiPaths.Order.GetUrlForProcessAnOrder(_remoteServiceBaseUrl);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, processAnOrderUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(orderViewModel), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            string responseString = await response.Content.ReadAsStringAsync();
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(responseString);
            return ticket ;
        }

        public async Task<bool> CancelAnOrder(int ticketId)
        {
            var cancelAnOrderUrl = ApiPaths.Order.GetUrlForCancelAnOrder(_remoteServiceBaseUrl);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, cancelAnOrderUrl)
            {
                Content = new StringContent(JsonConvert.SerializeObject(ticketId), System.Text.Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            if(response.StatusCode==HttpStatusCode.BadRequest)
            {
                return false;
            }
            return true;
        }

        public async Task<Ticket> GetTicketById(int ticketId)
        {
            var getTicketById = ApiPaths.Order.GetTicketById(_remoteServiceBaseUrl);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, getTicketById);

            var response = await _client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                return null;
            }
            string responseString = await response.Content.ReadAsStringAsync();
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(responseString);
            return ticket;
        }
        
    }
}
