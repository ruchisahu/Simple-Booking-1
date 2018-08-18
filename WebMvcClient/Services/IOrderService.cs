using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvcClient.Models;

namespace WebMvcClient.Services
{
    public interface IOrderService
    {
        Task<Ticket> ProcessAnOrder(OrderViewModel orderViewModel);
        Task<bool> CancelAnOrder(int ticketId);
        Task<Ticket> GetTicketById(int ticketId);

        Task<string> TestWelcome();
    }
}
