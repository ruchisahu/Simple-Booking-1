using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SearchEventAPI.Data;
using SearchEventAPI.Domain;

namespace SearchEventAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventsSearchController : Controller
    {
        [HttpGet]
        [Route("Search")]
        public List<int> GetEventsOnLocation([FromQuery] string location, [FromQuery] string eventType, [FromQuery] string eventCategory,[FromQuery] string evDate, [FromQuery] string priceType)
        {
            EventType? eventTypeParam = Convert<EventType>(eventType);
            EventCategory? eventCategoryParam = Convert<EventCategory>(eventCategory);
            EventPriceType? eventPriceTypeParam = Convert<EventPriceType>(priceType);

            var events = EventContext.GetEvents();

            return Search.SearchByLocation(events, location, eventTypeParam, eventCategoryParam, DateTime.Parse(evDate), eventPriceTypeParam);
        }

        // POST: api/Search
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Search/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
