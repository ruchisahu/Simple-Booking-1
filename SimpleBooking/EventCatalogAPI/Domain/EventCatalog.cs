using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCatalogAPI.Domain
{
    public enum EventType
    {
        Class = 0,
        Conference = 1,
        Seminar = 2,
        Tournament = 3,
        Game = 4,
        Festival = 5,
        Gathering = 6,
        Performance = 7,
        Tour = 8,
        Networking = 9,
        Party = 10,
        Race = 11,
        Expo = 12,
        Rally = 13,
        Convention = 14,
        Appearence = 15,
        Retreat = 16,
        Gala = 17,
        Attraction = 18,
        Other = 19
    }

    public enum EventPriceType
    {
        Free = 0,
        Paid = 1
    }

    public enum EventCategory
    {
        Music = 0,
        Kids = 1,
        Sports = 2,
        Fitness = 3,
        Tech = 4,
        Cooking = 5,
        Travel = 6,
        Fashion = 7,
        Holiday = 8,
        Business = 9,
        Health = 10,
        Other = 11,
        Home = 12,
        Lifestyle = 13,
        Family = 14,
        Education = 15,
        Outdoor = 16,
        Community = 17,
        Film = 18,
        School = 19,
        Auto = 20,
        Boat = 21,
        Air = 22,
        Hobbies = 23,
        Cause = 24,
        Charity = 25,
        Food = 26,
        Arts = 27
    }

    public class EventCatalog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("PlaceId")]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public EventPriceType PriceType { get; set; }
        public Decimal Price { get; set; }

        public string ImageURL { get; set; }
        public EventType Type { get; set; }

        public EventCategory Category { get; set; }

        public int InitialTicketCount { get; set; }
    }
}
