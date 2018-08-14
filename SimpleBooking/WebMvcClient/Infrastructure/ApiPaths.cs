using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class ApiPaths
    {
        public static class Catalog
        {
            public static string GetAllCatalogItems(string baseUrl, 
                int page, int take, int? Catagory, int? Place)
            {
                var filterQs = string.Empty;

                if (Catagory.HasValue || Place.HasValue)
                {
                    var brandQs = (Catagory.HasValue) ? Catagory.Value.ToString() : "null";
                    var typeQs = (Place.HasValue) ? Place.Value.ToString() : "null";
                    filterQs = $"/type/{typeQs}/brand/{brandQs}";
                }

                return $"{baseUrl}items{filterQs}?pageIndex={page}&pageSize={take}";
            }

            public static string GetCatalogItem(string baseUrl, int id)
            {

                return $"{baseUrl}/items/{id}";
            }
            public static string GetAllBrands(string baseUrl)
            {
                return $"{baseUrl}catalogBrands";
            }

            public static string GetCatalogItem(string baseUrl)
            {
                return $"{baseUrl}Place";
            }

            public static string GetAllTypes(string baseUrl)
            {
                return $"{baseUrl}catalogTypes";
            }

            internal static object GetAllCatalogItems(string remoteServiceBaseUrl, int page, int take, int catagory, int? place)
            {
                throw new NotImplementedException();
            }
        }
    }
}
