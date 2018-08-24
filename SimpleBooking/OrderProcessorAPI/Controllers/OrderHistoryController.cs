using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderProcessorAPI.Data;
using OrderProcessorAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessorAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EventManagement")]
    public class OrderHistoryController : Controller
    {
        private readonly OrderContext orderContext;

        public OrderHistoryController(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        [HttpGet]
        [Route("{id}/SalesTotal")]
        public IActionResult GetEventSalesTotal(int id)
        {
            int count = 0;
            decimal totalAmount = 0;
            Event _event = RetrieveEvent(id);

            if (_event == null)
            {
                return NotFound();
            }

            if (_event.Tickets != null)
            {
                foreach (Ticket ticket in _event.Tickets)
                {
                    if (ticket.Transactions != null)
                    {
                        foreach (Transaction transaction in ticket.Transactions)
                        {
                            count++;
                            totalAmount += transaction.TotalAmount;
                        }
                    }
                }
            }

            return Ok(Tuple.Create<int, decimal>(count, totalAmount));
        }

        [HttpGet]
        [Route("{id}/SalesHistory")]
        public IActionResult GetEventSalesHistory(int id)
        {
            Dictionary<DateTime, int> salesHistory = new Dictionary<DateTime, int>();
            Event _event = RetrieveEvent(id);

            if (_event == null)
            {
                return NotFound();
            }

            if (_event.Tickets != null)
            {
                foreach (Ticket ticket in _event.Tickets)
                {
                    if (ticket.Transactions != null)
                    {
                        foreach (Transaction transaction in ticket.Transactions)
                        {
                            DateTime date = transaction.ProcessingTime.Date;

                            if (salesHistory.ContainsKey(date))
                            {
                                salesHistory[date]++;
                            }
                            else
                            {
                                salesHistory.Add(date, 1);
                            }
                        }
                    }
                }
            }

            return Ok(salesHistory);
        }

        private Event RetrieveEvent(int id)
        {
            return orderContext.Events.Include(e => e.Tickets).ThenInclude(t => t.Transactions).SingleOrDefault(e => e.Id == id);
        }
    }
}