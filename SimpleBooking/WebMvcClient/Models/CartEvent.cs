using System.Collections.Generic;

namespace WebMvcClient.Models
{
    public class CartEvent
    {
        public List<CartEventItem> Items { get; set; }
    }
}
