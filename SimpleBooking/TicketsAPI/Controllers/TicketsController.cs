using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsAPI.Domain;
using TicketsAPI.Data;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private readonly TicketContext ticketContext;
        //private readonly IOptionsSnapshot<CatalogSettings> _settings;

        public TicketsController(TicketContext catalogContext)
        {
            ticketContext = catalogContext;
            //_settings = settings;
        }

        [HttpPost]
        [Route("Ticket")]
        public async Task<IActionResult> CreateTicket([FromBody]Ticket ticket)
        {
            ticketContext.Tickets.Add(ticket);
            await ticketContext.SaveChangesAsync();
            return await GetTicketById(ticket.Id);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var ticket = await ticketContext.Tickets.SingleOrDefaultAsync(c => c.Id == id);
            if (ticket != null)
            {
                return Ok(ticket);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("PurchasedTickets")]
        public async Task<IActionResult> GetPurchasedTickets([FromQuery] int eventId)
        {
            var purchasedTickets = await ticketContext.Tickets.SumAsync(t => t.EventId == eventId ? t.TicketsPurchased : 0);
            return Ok(purchasedTickets);
        }
    }
}