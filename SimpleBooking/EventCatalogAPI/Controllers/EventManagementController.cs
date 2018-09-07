using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EventManagement")]
    public class EventManagementController : Controller
    {
        private readonly EventCatalogContext eventCatalogContext;

        public EventManagementController(EventCatalogContext eventCatalogContext)
        {
            this.eventCatalogContext = eventCatalogContext;
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
            EventCatalog _event = eventCatalogContext.Eventcatalogs.Find(id);
            var place = eventCatalogContext.Places.Find(_event.PlaceId);

            if (_event == null)
            {
                return NotFound();
            }

            return Ok(_event);
        }

        [HttpPut]
        [Route("{id}/Edit/")]
        public IActionResult EditEvent(int id, string name, string description, Place place, DateTime? startDate, DateTime? endDate, EventPriceType? eventPriceType, Decimal? price, string imageURL, EventType? eventType, EventCategory? eventCategory, int? ticketCount)
        {
            EventCatalog match = eventCatalogContext.Eventcatalogs.Find(id);

            if (match == null)
            {
                return NotFound();
            }

            if (name != null && name != match.Name)
            {
                match.Name = name;
            }

            if (description != null && description != match.Description)
            {
                match.Description = description;
            }

            if (place != null && place != match.Place)
            {
                match.Place = place;
            }

            if (startDate != null && startDate != match.StartDate)
            {
                match.StartDate = (DateTime)startDate;
            }

            if (endDate != null && endDate != match.EndDate)
            {
                match.EndDate = (DateTime)endDate;
            }

            if (eventPriceType != null && eventPriceType != match.PriceType)
            {
                match.PriceType = (EventPriceType)eventPriceType;
            }

            if (price != null && price != match.Price)
            {
                match.Price = (Decimal)price;
            }

            if (imageURL != null && imageURL != match.ImageURL)
            {
                match.ImageURL = imageURL;
            }

            if (eventType != null & eventType != match.Type)
            {
                match.Type = (EventType)eventType;
            }

            if (eventCategory != null & eventCategory != match.Category)
            {
                match.Category = (EventCategory)eventCategory;
            }

            if (ticketCount != null && ticketCount != match.InitialTicketCount)
            {
                match.InitialTicketCount = (int)ticketCount;
            }

            eventCatalogContext.SaveChanges();

            return Ok(match);
        }
    }
}