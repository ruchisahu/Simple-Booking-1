using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            //test data	
            EventCatalog _event = new EventCatalog();
            Place place = new Place();

            _event.Name = "Tech Social";
            _event.Description = "Neckbeards";
            _event.Place = place;
            _event.StartDate = DateTime.Now;
            _event.EndDate = DateTime.Now.AddDays(1);
            _event.PriceType = EventPriceType.Free;
            _event.Price = 292m;
            _event.ImageURL = "google.com";
            _event.Type = EventType.Appearence;
            _event.Category = EventCategory.Air;
            _event.InitialTicketCount = 100;

            place.Address = "111 House Street";
            place.City = "Seattle";
            place.State = "WA";
            place.ZipCode = 98101;
            place.PlaceName = "John Wayne";
            place.PlacePrice = 100m;

            eventCatalogContext.Eventcatalogs.Add(_event);
            eventCatalogContext.SaveChanges();
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
            EventCatalog _event = eventCatalogContext.Eventcatalogs.Find(id); //issue with swagger not showing nested items in json response

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