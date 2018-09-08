using System.Collections.Generic;
using WebMvcClient.Models;

namespace WebMvcClient.ViewModels
{
    public class CartListModel
    {
        public List<Event> EventsInCart { get; set; }

        public decimal Total { get; set; }
    }
}
