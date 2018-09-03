namespace WebMvcClient.Infrastructure
{
    public class ApiPaths
    {
        public static class Order
        {
            public static string GetUrlForWelcome(string baseUri)
            {
                return $"{baseUri}/welcome";
            }

            public static string GetUrlForProcessAnOrder(string baseUri)
            {
                return $"{baseUri}/processanorder";
            }
            public static string GetTicketById(string baseUri)
            {
                return $"{baseUri}/gettickeybyid";
            }
            public static string GetUrlForCancelAnOrder(string baseUri)
            {
                return $"{baseUri}/cancelanorder";
            }
        }

        public static class Basket
        {
            public static string GetBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }

            public static string UpdateBasket(string baseUri)
            {
                return baseUri;
            }

            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }

        public static class EventsSearch
        {
            public static string GetEvents(string baseUri, string location, string eventType, string eventCategory, string priceType)
            {
                return $"{baseUri}/Events?location={location}&anyText={location}&eventType={eventType}&eventCategory={eventCategory}&priceType={priceType}";
            }

            public static string GetCategories(string baseUri)
            {
                return $"{baseUri}/Categories";
            }

            public static string GetTypes(string baseUri)
            {
                return $"{baseUri}/Types";
            }

            public static string GetPrices(string baseUri)
            {
                return $"{baseUri}/Prices";
            }
        }

        public static class EventManagement
        {
            public static string GetEvent(string baseUri, int eventId)
            {
                return $"{baseUri}/{eventId}";
            }
        }

        public static class Image
        {
            public static string GetImageUrl(string baseUri, string image)
            {
                return $"{baseUri}/{image}";
            }
        }
    }
}
