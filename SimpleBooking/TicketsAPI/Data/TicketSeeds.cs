using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace TicketsAPI.Data
{
    public class TicketSeeds
    {
       public static async Task SeedAsync(TicketContext context)
        {
            context.Database.Migrate();
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange
                    (GetPreconfiguredTickets());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Ticket> GetPreconfiguredTickets()
        {
            return new List<Ticket>()
            {
                new Ticket(){ EventId = 1, TicketsPurchased = 10},
                new Ticket(){ EventId = 2, TicketsPurchased = 5},
                new Ticket(){ EventId = 5, TicketsPurchased = 15},
                new Ticket(){ EventId = 20, TicketsPurchased = 20},
                new Ticket(){ EventId = 9, TicketsPurchased = 100},
                new Ticket(){ EventId = 3, TicketsPurchased = 25},
                new Ticket(){ EventId = 7, TicketsPurchased = 18}
            };
        }
    }
}

