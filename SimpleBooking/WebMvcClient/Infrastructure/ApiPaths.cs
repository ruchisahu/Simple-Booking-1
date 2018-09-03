using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
