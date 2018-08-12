using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/EventsSearch")]
    public class EventsSearchController : Controller
    {
        private readonly EventCatalogContext eventCatalogContext;

        public EventsSearchController(EventCatalogContext eventContext)
        {
            eventCatalogContext = eventContext;
        }

        //[HttpGet]
        //[Route("[action]")]
        //public async Task<ActionResult> SearchEvents([FromQuery] string location, [FromQuery] string eventType, [FromQuery] string eventCategory, [FromQuery] string evDate, [FromQuery] string priceType, [FromQuery] string anyText)
        //{
        //    EventType? eventTypeParam = Convert<EventType>(eventType);
        //    EventCategory? eventCategoryParam = Convert<EventCategory>(eventCategory);
        //    EventPriceType? eventPriceTypeParam = Convert<EventPriceType>(priceType);

        //    DateTime? eventDate;
        //    if (string.IsNullOrWhiteSpace(evDate))
        //    {
        //        eventDate = null;
        //    }
        //    else
        //    {
        //        eventDate = DateTime.Parse(evDate);
        //    }

        // var events = await EventContext.GetEvents();

        //    var eventIds = Search.SearchEvents(events, location, eventTypeParam, eventCategoryParam, eventDate, eventPriceTypeParam, anyText);

        //    return Ok(eventIds);
        //}

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> SearchEvents([FromQuery] string location, [FromQuery] string eventType, [FromQuery] string eventCategory, [FromQuery] string evDate, [FromQuery] string priceType, [FromQuery] string anyText)
        {
            // EventType? eventTypeParam = Convert<EventType>(eventType);
            EventCategory? eventCategoryParam = Convert<EventCategory>(eventCategory);
            EventPriceType? eventPriceTypeParam = Convert<EventPriceType>(priceType);

            DateTime? eventDate;
            if (string.IsNullOrWhiteSpace(evDate))
            {
                eventDate = null;
            }
            else
            {
                eventDate = DateTime.Parse(evDate);
            }

            var places = await eventCatalogContext.Places.ToListAsync();
            var events = await eventCatalogContext.Eventcatalogs.ToListAsync();

            var eventIds = Search.SearchEvents(events, location, eventCategoryParam, eventDate, eventPriceTypeParam, anyText);

            return Ok(eventIds);
        }

        private T? Convert<T>(string input) where T : struct
        {
            T? returnType = null;
            T tempType;
            if (Enum.TryParse<T>(input, true, out tempType))
            {
                returnType = tempType;
            }

            return returnType;
        }
    }
}