using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{

    public class DbInitializer
    {
        public static async Task SeedAsync(EventCatalogContext context)
        {
            context.Database.Migrate();
            if (!context.Places.Any())
            {
                context.Places.AddRange
                    (GetPreconfiguredPlaces());
                await context.SaveChangesAsync();
            }
            if (!context.Tickets.Any())
            {
                context.Tickets.AddRange
                    (GetPreconfiguredTickets());
                context.SaveChanges();
            }
            if (!context.Users.Any())
            {
                context.Users.AddRange
                    (GetPreconfiguredUsers());
                context.SaveChanges();
            }
            if (!context.Eventcatalogs.Any())
            {
                context.Eventcatalogs.AddRange
                    (GetPreconfiguredevent());
                context.SaveChanges();
            }
        }
        static IEnumerable<Place> GetPreconfiguredPlaces()
        {
            return new List<Place>()
            {
                new Place
                {
                    PlaceId = 1,
                    PlaceName = "Mountain",
                    Address = "Tiger Mountain",
                    City = "Issaquah",
                    State = "WA",
                    ZipCode = 98027,
                    PlacePrice = 0
                },
                new Place
                {
                    PlaceId = 2,
                    PlaceName = "Bellevue College",
                    Address = "Main Street",
                    City = "Bellevue",
                    State = "WA",
                    ZipCode = 98048,
                    PlacePrice = 500
                },
                new Place
                {
                    PlaceId = 3,
                    PlaceName = "Sammamish Lake",
                    Address = "Lake Sammamish Street",
                    City = "Sammamish",
                    State = "WA",
                    ZipCode = 98075,
                    PlacePrice = 760
                }
            };
        }
        static IEnumerable<Ticket> GetPreconfiguredTickets()
        {
            return new List<Ticket>()
            {
            };
        }
        static IEnumerable<User> GetPreconfiguredUsers()
        {
            /*  List<Eventcatalog> Event1 = new List<Eventcatalog>();
              Event1.Add(new Eventcatalog()  );*/
            return new List<User>()
            {

                //  new User() { UserId=2, Password= "a",Email="test1@test1",Name="joe",BillingAddress="address1",TicketId=1, CreditCardNo=456,EventId=1},

            };
        }

        static IEnumerable<EventCatalog> GetPreconfiguredevent()
        {
            return new List<EventCatalog>()
            {
                new EventCatalog
                {
                    Id = 1,
                    Name = "Camping",
                    Description = "Sleeping in a tent outdoor",
                    PlaceId = 1,
                    StartDate = DateTime.Parse("8/12/2018"),
                    EndDate = DateTime.Parse("8/12/2019"),
                    PriceType = EventPriceType.Free,
                    Price = 0,
                    ImageURL = "large_gonecamping-1.png",
                    Type = EventType.Party,
                    Category = EventCategory.Outdoor,
                    InitialTicketCount = 100
                },
                new EventCatalog
                {
                    Id = 2,
                    Name = "Java Class",
                    Description = "Learn Java",
                    PlaceId = 2,
                    StartDate = DateTime.Parse("8/11/2018"),
                    EndDate = DateTime.Parse("8/11/2019"),
                    PriceType = EventPriceType.Free,
                    Price = 0,
                    ImageURL = "java.png",
                    Type = EventType.Seminar,
                    Category = EventCategory.Tech,
                    InitialTicketCount = 200
                },
                new EventCatalog
                {
                    Id = 3,
                    Name = "DotNet Class",
                    Description = "Learn DotNet",
                    PlaceId = 2,
                    StartDate = DateTime.Parse("9/11/2018"),
                    EndDate = DateTime.Parse("9/11/2019"),
                    PriceType = EventPriceType.Free,
                    Price = 0,
                    ImageURL = "dotnet-1.png",
                    Type = EventType.Seminar,
                    Category = EventCategory.Tech,
                    InitialTicketCount = 300
                },
                new EventCatalog
                {
                    Id = 4,
                    Name = "php Class",
                    Description = "Learn php",
                    PlaceId = 2,
                    StartDate = DateTime.Parse("8/14/2018"),
                    EndDate = DateTime.Parse("9/11/2019"),
                    PriceType = EventPriceType.Free,
                    Price = 0,
                    ImageURL = "php-1.png",
                    Type = EventType.Seminar,
                    Category = EventCategory.Tech,
                    InitialTicketCount = 400
                },
                new EventCatalog
                {
                    Id = 5,
                    Name = "perl Class",
                    Description = "Learn perl",
                    PlaceId = 2,
                    StartDate = DateTime.Parse("8/14/2018"),
                    EndDate = DateTime.Parse("8/16/2018"),
                    PriceType = EventPriceType.Free,
                    Price = 0,
                    ImageURL = "perl-1.png",
                    Type = EventType.Seminar,
                    Category = EventCategory.Tech,
                    InitialTicketCount = 500
                },
                new EventCatalog
                {
                    Id = 6,
                    Name = "Fishing",
                    Description = "Salmon fishing",
                    PlaceId = 3,
                    StartDate = DateTime.Parse("8/19/2018"),
                    EndDate = DateTime.Parse("8/27/2018"),
                    PriceType = EventPriceType.Paid,
                    Price = 760,
                    ImageURL = "fishing-1.png",
                    Type = EventType.Gathering,
                    Category = EventCategory.Outdoor,
                    InitialTicketCount = 75
                }
            };
        }
    }
}
