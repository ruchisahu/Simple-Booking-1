namespace EventManagementAPI.Domain
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public decimal Price { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }
    }
}
