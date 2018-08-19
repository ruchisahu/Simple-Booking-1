using System;

namespace WebMvcClient.Models
{
    public enum EventType { Class, Conference, Seminar, Tournament, Game, Festival, Gathering, Performance, Tour, Networking, Party, Race, Expo, Rally, Convention, Appearence, Retreat, Gala, Attraction, Other };
    public enum EventPriceType { Free, Paid }

    public enum EventCategory { Music, Kids, Sports, Fitness, Tech, Cooking, Travel, Fashion, Holiday, Business, Health, Other, Home, Lifestyle, Family, Education, Outdoor, Community, Film, School, Auto, Boat, Air, Hobbies, Cause, Charity, Food, Arts }

    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }

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
