using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> SearchEvents([FromQuery] string location, [FromQuery] string eventType,
                                                    [FromQuery] string eventCategory, [FromQuery] string eventStartDate,
                                                    [FromQuery] string eventEndDate, [FromQuery] string priceType, [FromQuery] string anyText,
                                                    [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var totalEventsCount = await eventCatalogContext.Eventcatalogs.LongCountAsync();
            var itemOnPage = await eventCatalogContext.Eventcatalogs.OrderBy(ev => ev.Name)
                                                                    .Skip(pageIndex * pageSize)
                                                                    .Take(pageSize).ToListAsync();

            EventType? eventTypeParam = Convert<EventType>(eventType);
            EventCategory? eventCategoryParam = Convert<EventCategory>(eventCategory);
            EventPriceType? eventPriceTypeParam = Convert<EventPriceType>(priceType);

            DateTime? startDate;
            if (string.IsNullOrWhiteSpace(eventStartDate))
            {
                startDate = null;
            }
            else
            {
                startDate = DateTime.Parse(eventStartDate);
            }

            DateTime? endDate;
            if (string.IsNullOrWhiteSpace(eventEndDate))
            {
                endDate = null;
            }
            else
            {
                endDate = DateTime.Parse(eventEndDate);
            }

            var places = await eventCatalogContext.Places.ToListAsync();
            var events = await eventCatalogContext.Eventcatalogs.ToListAsync();

            var eventIds = Search.SearchEvents(events, location, eventTypeParam, eventCategoryParam, startDate, endDate, eventPriceTypeParam, anyText);

            return Ok(eventIds);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> Events([FromQuery] string location, [FromQuery] string eventType,
                                                    [FromQuery] string eventCategory, [FromQuery] string eventStartDate,
                                                    [FromQuery] string eventEndDate, [FromQuery] string priceType, [FromQuery] string anyText,
                                                    [FromQuery] int pageSize = 6, [FromQuery] int pageIndex = 0)
        {
            var totalEventsCount = await eventCatalogContext.Eventcatalogs.LongCountAsync();
            var itemOnPage = await eventCatalogContext.Eventcatalogs.OrderBy(ev => ev.Name)
                                                                    .Skip(pageIndex * pageSize)
                                                                    .Take(pageSize).ToListAsync();

            EventType? eventTypeParam = Convert<EventType>(eventType);
            EventCategory? eventCategoryParam = Convert<EventCategory>(eventCategory);
            EventPriceType? eventPriceTypeParam = Convert<EventPriceType>(priceType);

            DateTime? startDate;
            if (string.IsNullOrWhiteSpace(eventStartDate))
            {
                startDate = null;
            }
            else
            {
                startDate = DateTime.Parse(eventStartDate);
            }

            DateTime? endDate;
            if (string.IsNullOrWhiteSpace(eventEndDate))
            {
                endDate = null;
            }
            else
            {
                endDate = DateTime.Parse(eventEndDate);
            }

            var places = await eventCatalogContext.Places.ToListAsync();
            var events = await eventCatalogContext.Eventcatalogs.ToListAsync();

            var eventsSearched = Search.NewSearch(events, location, eventTypeParam, eventCategoryParam, startDate, endDate, eventPriceTypeParam, anyText);

            foreach (var ev in eventsSearched)
            {
                ev.ImageURL = $"http://localhost:49572/api/image/{ev.ImageURL}";
            }

            var eventModel = new PaginatedItemsViewModel<EventCatalog>(pageIndex, pageSize, totalEventsCount, eventsSearched);
            return Ok(eventModel);
        }

        [HttpGet]
        [Route("[action]")]
        public List<string> Categories()
        {
            var categories = new List<string>(Enum.GetNames(typeof(EventCategory)));
            return categories;
        }

        [HttpGet]
        [Route("[action]")]
        public List<string> Types()
        {
            var types = new List<string>(Enum.GetNames(typeof(EventType)));
            return types;
        }

        [HttpGet]
        [Route("[action]")]
        public List<string> Prices()
        {
            var prices = new List<string>(Enum.GetNames(typeof(EventPriceType)));
            return prices;
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