using EventManagementAPI.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EventManagementAPI.Data
{
    public class ManagementContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ManagementContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>(ConfigureEvent);
            builder.Entity<Place>(ConfigurePlace);
            builder.Entity<Ticket>(ConfigureTicket);
            builder.Entity<Transaction>(ConfigureTransaction);
        }

        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.Property(e => e.Id).ForSqlServerUseSequenceHiLo("Event_hilo").IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.PriceType).IsRequired();
            builder.Property(e => e.Category).IsRequired();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.Price).IsRequired();
            builder.Property(e => e.ImageURL).IsRequired();
        }

        private void ConfigurePlace(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Place");
            builder.Property(p => p.Id).ForSqlServerUseSequenceHiLo("Place_hilo").IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.State).IsRequired();
            builder.Property(p => p.ZipCode).IsRequired();
            builder.Property(p => p.Price).IsRequired();
        }

        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.Property(t => t.Id).ForSqlServerUseSequenceHiLo("Ticket_hilo").IsRequired();
            builder.Property(t => t.Quantity).IsRequired();

            builder.HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId);
        }

        private void ConfigureTransaction(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.Property(t => t.Id).ForSqlServerUseSequenceHiLo("Transaction_hilo").IsRequired();
            builder.Property(t => t.TotalAmount).IsRequired();
            builder.Property(t => t.ProcessingTime).IsRequired();

            builder.HasOne(t => t.Ticket)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.TicketId);
        }
    }
}
