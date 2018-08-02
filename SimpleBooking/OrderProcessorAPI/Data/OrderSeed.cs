using Microsoft.EntityFrameworkCore;
using OrderProcessorAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Data
{
    /// <summary>
    /// This class whole purpose is to provide seed data to do testing when database and tables are up and running
    /// </summary>
    public class OrderSeed
    {
        public static async SeedAsync(OrderContext context)
        {
            context.Database.Migrate();
            if(!context.Users.Any())
            {
                context.Users.AddRange(GetPreconfiguredUsers());
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(GetPreconfiguredEvents());
                context.SaveChanges();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange(GetPreconfiguredTickets());
                context.SaveChanges();
            }
        }
        //Seed data for User table
        static IEnumerable<User>GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User(){Name="John Smith", BillingAddress="2121,232nd st SE Forks WA 98053", CreditCardNo=4242424242424242, CSC=323, EmailAddress="test@test.com", ExpirationDate="09/2024"},
                new User() {Name = "David Simon", BillingAddress = "2021,212nd st SE Barn VA 51123", CreditCardNo = 4000056655665556, CSC = 355, EmailAddress = "test1@test.com", ExpirationDate = "11/2024" }
        };
        }

        //Seed data for Event table
        static IEnumerable<Event>GetPreconfiguredEvents()
        {
            return new List<Event>()
            {
                new Event(){Name="WebDevlopment BootCamp", Location="15800 Bear Creek Parkway Forks WA 98054", Price=97},
                new Event(){Name="FitFest 2019", Location="Mill Creek Town Center NorthBend WA 98034", Price=25}
            };
        }
        //Seed data for Ticket table
        static IEnumerable<Ticket>GetPreconfiguredTickets()
        {
            return new List<Ticket>()
            {
                new Ticket(){UserId=2, EventId=1, Quantity=2, AuthCode="1234c", TicketCreationTime="7/31/2018 4:05:04 PM", TotalAmount=50},
                new Ticket(){UserId=1, EventId=2, Quantity=1, AuthCode="3456d", TicketCreationTime="8/1/2018 2:10:08 PM", TotalAmount=97},
                new Ticket(){UserId=1, EventId=1, Quantity=3, AuthCode="3421f", TicketCreationTime="8/2/2018 10:15:09 AM", TotalAmount=75},
            };
        }
    }
}
