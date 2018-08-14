using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMvc.Models
{
    public class CatalogItem
    {
     //   public string Id { get; set; }
        public string EventName { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string PictureUrl { get; set; }
        public string EventCategory { get; set; }

    }
}
