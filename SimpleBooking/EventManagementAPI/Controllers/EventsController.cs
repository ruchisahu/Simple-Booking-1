using EventManagementAPI.Data;
using EventManagementAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsController : Controller
    {
        private readonly ManagementContext managementContext;

        public EventsController(ManagementContext managementContext)
        {
            this.managementContext = managementContext;
        }

        [HttpGet]
        [Route("Launch")]
        public IActionResult Launch()
        {
            return Ok("You have successfully reached the Event Management API!");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEvent(int id)
        {
            //Event _event = RetrieveEvent(id);
            Event _event = managementContext.Events.Find(id); //issue with swagger not showing nested items in json response

            if(_event == null)
            {
                return NotFound();
            }

            return Ok(_event);
        }

        [HttpGet]
        [Route("{id}/Stats/SalesTotal")]
        public IActionResult GetEventSalesTotal(int id)
        {
            int count = 0;
            decimal totalAmount = 0;
            Event _event = RetrieveEvent(id);

            if (_event == null)
            {
                return NotFound();
            }

            if(_event.Tickets != null)
            {
                foreach(Ticket ticket in _event.Tickets)
                {
                    if(ticket.Transactions != null)
                    {
                        foreach(Transaction transaction in ticket.Transactions)
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
        [Route("{id}/Stats/SalesHistory")]
        public IActionResult GetEventSalesHistory(int id)
        {
            Dictionary<DateTime, int> salesHistory = new Dictionary<DateTime, int>();
            Event _event = RetrieveEvent(id);

            if (_event == null)
            {
                return NotFound();
            }

            if(_event.Tickets != null)
            {
                foreach(Ticket ticket in _event.Tickets)
                {
                    if(ticket.Transactions != null)
                    {
                        foreach(Transaction transaction in ticket.Transactions)
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

        [HttpPut]
        [Route("{id}/Edit/")]
        public IActionResult EditEvent(int id, string name, string description, EventPriceType priceType, EventCategory eventCategory, DateTime? date, Decimal? price, string imageURL)
        {
            Event match = managementContext.Events.Find(id);

            if (match == null)
            {
                return NotFound();
            }

            if(name != null && name != match.Name)
            {
                match.Name = name;
            }

            if (description != null && description != match.Description)
            {
                match.Description = description;
            }

            if (priceType != match.PriceType)
            {
                match.PriceType = priceType;
            }

            if (eventCategory != match.Category)
            {
                match.Category = eventCategory;
            }

            if (date != null && date != match.Date)
            {
                match.Date = (DateTime)date;
            }

            if (price != null && price != match.Price)
            {
                match.Price = (Decimal)price;
            }

            if (imageURL != null && imageURL != match.ImageURL)
            {
                match.ImageURL = imageURL;
            }

            managementContext.SaveChanges();

            return Ok(match);
        }

        private Event RetrieveEvent(int id)
        {
            return managementContext.Events.Include(e => e.Tickets).ThenInclude(t => t.Transactions).SingleOrDefault(e => e.Id == id);
        }
    }
}