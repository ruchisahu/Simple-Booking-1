using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketsAPI.Domain;

namespace TicketsAPI.Data
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.OnModelCreating(builder);
            builder.Entity<Ticket>(ConfigureTicket);
        }

        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            // throw new NotImplementedException();
            builder.ToTable("Ticket");
            builder.Property(c => c.Id).ForSqlServerUseSequenceHiLo("ticket_hilo").IsRequired();
            builder.Property(c => c.EventId).IsRequired();
            builder.Property(c => c.TicketsPurchased).IsRequired();
            
        }
    }
}
