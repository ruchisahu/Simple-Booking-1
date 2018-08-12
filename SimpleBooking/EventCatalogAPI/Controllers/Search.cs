using EventCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCatalogAPI.Controllers
{
    public class Search
    {
        //public static List<int> SearchEvents(List<Eventcatalog> events, string location, EventType? evType, EventCategory? evCategory, DateTime? evDate, EventPriceType? priceType, string anyText)
        //{
        //    // List<int> ids;
        //    //return (from e in events
        //    //              where e.Location == location
        //    //              select e.ID).ToList();
        //    var filteredEvents = events.Where(ev => string.Equals(ev.Location, location, StringComparison.OrdinalIgnoreCase));
        //    if (evType.HasValue)
        //    {
        //        filteredEvents = filteredEvents.Where(ev => ev.Type == evType);
        //    }
        //    if (evCategory.HasValue)
        //    {
        //        filteredEvents = filteredEvents.Where(ev => ev.Category == evCategory);
        //    }
        //    if (evDate.HasValue)
        //    {
        //        filteredEvents = filteredEvents.Where(ev => ev.EventDate == evDate);
        //    }
        //    if (priceType.HasValue)
        //    {
        //        filteredEvents = filteredEvents.Where(ev => ev.PriceType == priceType);
        //    }
        //    if (anyText != null)
        //    {
        //        filteredEvents = filteredEvents.Where(ev => (ev.Description.IndexOf(anyText, StringComparison.OrdinalIgnoreCase) > -1)
        //                            || (ev.Name.IndexOf(anyText, StringComparison.OrdinalIgnoreCase)) > -1);
        //    }


        //    return filteredEvents.Select(ev => ev.ID).ToList();
        //}

        public static List<int> SearchEvents(List<Eventcatalog> events, string location, EventCategory? evCategory, DateTime? evDate, EventPriceType? priceType, string anyText)
        {
            // List<int> ids;
            //return (from e in events
            //              where e.Location == location
            //              select e.ID).ToList();
            var filteredEvents = events.Where(ev => string.Equals(ev.Place.PlaceName, location, StringComparison.OrdinalIgnoreCase));
            //if (evType.HasValue)
            //{
            //    filteredEvents = filteredEvents.Where(ev => ev.Type == evType);
            //}

            if (evCategory.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.EventCategory == evCategory);
            }
            if (evDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.EventDate == evDate);
            }
            if (priceType.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.EventPriceType == priceType);
            }
            if (anyText != null)
            {
                filteredEvents = filteredEvents.Where(ev => (ev.Description.IndexOf(anyText, StringComparison.OrdinalIgnoreCase) > -1)
                                    || (ev.EventName.IndexOf(anyText, StringComparison.OrdinalIgnoreCase)) > -1);
            }


            return filteredEvents.Select(ev => ev.EventId).ToList();
        }

        //public static List<int> SearchByTypeLocation(List<Event> events, EventType evType, string location)
        //{
        //    // List<int> ids;
        //    //return (from e in events
        //    //        where (e.Location == location) && (e.Type == evType)
        //    //        select e.ID).ToList();
        //    return events?.Where(ev => string.Equals(ev.Location, location, StringComparison.OrdinalIgnoreCase) && ev.Type == evType).Select(ev => ev.ID).ToList();
        //}

        //public static List<int> SearchByCategoryLocation(List<Event> events, EventCategory evCategory, string location)
        //{
        //    // List<int> ids;
        //    //return (from e in events
        //    //        where (e.Location == location) && (e.Type == eventType)
        //    //        select e.ID).ToList();
        //    return events?.Where(ev => ev.Location == location && ev.Category == evCategory).Select(ev => ev.ID).ToList();
        //}

        //public static List<int> SearchByDateLocation(List<Event> events, DateTime evTime, string location)
        //{
        //    return events?.Where(ev => ev.Location == location && ev.EventDate == evTime).Select(ev => ev.ID).ToList();
        //}

        //public static List<int> SearchByPriceType(List<Event> events, EventPriceType price, string location)
        //{
        //    return events?.Where(ev => ev.Location == location && ev.PriceType == price).Select(ev => ev.ID).ToList();
        //}

        //public static List<int> SearchByTextLocation(List<Event> events, string textQuery, string location)
        //{
        //    return events?.Where(ev => ev.Location == location && ((ev.Description.IndexOf(textQuery, StringComparison.OrdinalIgnoreCase)) > -1
        //        || (ev.Name.IndexOf(textQuery, StringComparison.OrdinalIgnoreCase)) > -1)).Select(ev => ev.ID).ToList();
        //}
    }
}
