using EventManagementAPI.Data;
using EventManagementAPI.Domain;
using Microsoft.AspNetCore.Mvc;
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

            //test data
            Event _event = new Event();
            Place place = new Place();
            List<Ticket> tickets = new List<Ticket>();

            _event.Category = EventCategory.Tech;
            _event.Date = DateTime.Now;
            _event.Description = "A networking event for the tech community";
            _event.ImageURL = "https://www.google.com";
            _event.Name = "Tech Social";
            _event.Place = place;
            _event.Price = 292;
            _event.PriceType = EventPriceType.Paid;
            _event.Tickets = tickets;

            place.Address = "111 House Street";
            place.City = "Seattle";
            place.Name = "Your House";
            place.Price = 199;
            place.State = "WA";
            place.ZipCode = 98101;

            managementContext.Events.Add(_event);
            managementContext.SaveChanges();
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
            Event _event = managementContext.Events.Find(id);

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
            Event _event = managementContext.Events.SingleOrDefault(x => x.Id == id); //event is a keyword hence the underscore

            if(_event == null)
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
            Event _event = managementContext.Events.SingleOrDefault(x => x.Id == id);

            if(_event == null)
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

                            if (!salesHistory.ContainsKey(date))
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
        public IActionResult EditEvent(Event _event)
        {
            Event match = managementContext.Events.SingleOrDefault(x => x.Id == _event.Id);

            if(match == null)
            {
                return NotFound();
            }

            match = _event;
            managementContext.SaveChanges();

            return Ok(match);
        }
    }
}