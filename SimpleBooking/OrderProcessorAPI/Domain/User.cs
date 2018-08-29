using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Domain
{
    public class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        public string BuyerId { get; set; }

        public string Name { get; set; }

        public string BillingAddress { get; set; }

        public string EmailAddress { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        

    }
}
