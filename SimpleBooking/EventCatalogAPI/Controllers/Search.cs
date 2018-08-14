using EventCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCatalogAPI.Controllers
{
    public class Search
    {
        public static List<int> SearchEvents(List<EventCatalog> events, string location, EventType? evType, EventCategory? evCategory, DateTime? startDate, DateTime? endDate, EventPriceType? priceType, string anyText)
        {
            var filteredEvents = events.Where(ev => Contains(ev.Place.PlaceName, location)
                                                || Contains(ev.Place.Address, location)
                                                || Contains(ev.Place.City, location)
                                                || Contains(ev.Name, location)
                                                || Contains(ev.Description, location));
            if (evType.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.Type == evType);
            }

            if (evCategory.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.Category == evCategory);
            }

            if (!startDate.HasValue && !endDate.HasValue)
            {
                var today = Today();
                filteredEvents = filteredEvents.Where(ev => ev.StartDate <= today && today <= ev.EndDate);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => startDate <= ev.EndDate && ev.StartDate <= endDate);
            }

            if (priceType.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.PriceType == priceType);
            }
            if (anyText != null)
            {
                filteredEvents = filteredEvents.Where(ev => Contains(ev.Description, anyText) || Contains(ev.Name, anyText));
            }

            return filteredEvents.Select(ev => ev.Id).ToList();
        }

        public static List<EventCatalog> NewSearch(List<EventCatalog> events, string location, EventType? evType, EventCategory? evCategory, DateTime? startDate, DateTime? endDate, EventPriceType? priceType, string anyText)
        {
            var filteredEvents = events.Where(ev => Contains(ev.Place.PlaceName, location)
                                                || Contains(ev.Place.Address, location)
                                                || Contains(ev.Place.City, location)
                                                || Contains(ev.Name, location)
                                                || Contains(ev.Description, location));
            if (evType.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.Type == evType);
            }

            if (evCategory.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.Category == evCategory);
            }

            if (!startDate.HasValue && !endDate.HasValue)
            {
                var today = Today();
                filteredEvents = filteredEvents.Where(ev => ev.StartDate <= today && today <= ev.EndDate);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => startDate <= ev.EndDate && ev.StartDate <= endDate);
            }

            if (priceType.HasValue)
            {
                filteredEvents = filteredEvents.Where(ev => ev.PriceType == priceType);
            }
            if (anyText != null)
            {
                filteredEvents = filteredEvents.Where(ev => Contains(ev.Description, anyText) || Contains(ev.Name, anyText));
            }

            return filteredEvents.ToList();
        }

        private static bool Contains(string a, string b)
        {
            return !string.IsNullOrEmpty(a) && a.IndexOf(b, StringComparison.OrdinalIgnoreCase) > -1;
        }

        private static DateTime Today()
        {
            var now = DateTime.UtcNow;
            return new DateTime(now.Year, now.Month, now.Day);
        }
    }
}
