using System.Collections.Generic;

namespace WebMvcClient.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartTicket> Items { get; set; }
    }
}
