using System.Collections.Generic;

namespace CartAPI.Domain
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<Ticket> Items { get; set; }

        public Cart(string cartId)
        {
            BuyerId = cartId;
            Items = new List<Ticket>();
        }
    }
}
