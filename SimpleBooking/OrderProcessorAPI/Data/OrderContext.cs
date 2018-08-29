using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderProcessorAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessorAPI.Data
{
    /// <summary>
    /// This class contains instructions for EntityCore Framework. OrderContext is the DB name for OrderProcessor API.
    /// </summary>
    public class OrderContext:DbContext
    {
        //Dependency Injection
        public OrderContext(DbContextOptions options) : base(options)
        {
        }

        //In OrderContext database this how we will declare three tables
        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //These methods will create table with schema defined in methods passed to Entity<> or definition of entity 
            builder.Entity<Event>(ConfigureEvent);
            builder.Entity<User>(ConfigureUser);
            builder.Entity<Ticket>(ConfigureTicket);
            builder.Entity<Transaction>(ConfigureTransaction);
        }

        private void ConfigureEvent(EntityTypeBuilder<Event> builder)
        {
            //Creating table Event
            //Define name of the table, generate primary key column 
            builder.ToTable("Event");
            builder.Property(e => e.Id)
                .ForSqlServerUseSequenceHiLo("Event_hilo")
                .IsRequired();

            //Creating column "Name"
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            //Creating column "Location"
            builder.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(100);

            //creating column "Price"
            builder.Property(e => e.Price)
                .IsRequired();

            //creating column "EventDate"
            builder.Property(e => e.EventDate)
                .HasDefaultValue(DateTime.Now);

        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            //Creating table User
            //Define name of the table, creating Primary key column 
            builder.ToTable("User");

            //Creating column "Id"
            builder.Property(u => u.Id)
                .ForSqlServerUseSequenceHiLo("User_hilo")
                .IsRequired();

            //Creating column "Name" 
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            //Creating column "BillingAddress"
            builder.Property(u => u.BillingAddress)
                .IsRequired()
                .HasMaxLength(100);

            //Creating column "EmailAddress"
            builder.Property(e => e.EmailAddress)
                .IsRequired();

            //Creating column "BuyerId"
            builder.Property(e => e.BuyerId)
                .IsRequired();

        }

        private void ConfigureTicket(EntityTypeBuilder<Ticket> builder)
        {
            //Name of the table
            builder.ToTable("Ticket");

            //Creating column "Id"
            builder.Property(t => t.Id)
                .IsRequired();

            //Creating column "Quantity"
            builder.Property(t => t.Quantity)
                .IsRequired();

            //Creating column "TicketStatus"
            builder.Property(t=>t.Status)
                .IsRequired();

            // Define foreign key relation between Ticket and Event tables.
            builder.HasOne(t => t.Event)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define foreign key relation between Ticket and User tables.
            builder.HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        private void ConfigureTransaction(EntityTypeBuilder<Transaction> builder)
        {
            //Name of the table
            builder.ToTable("Transaction");

            //Creating column "Id"
            builder.Property(t => t.Id)
                .IsRequired();

            //Creating column "AuthCode"
            builder.Property(t => t.AuthCode)
                .IsRequired();

            //Creating column "Total Amount"
            builder.Property(t => t.TotalAmount)
                .IsRequired();

            //Creating column "TicketCreationTime"
            builder.Property(t => t.ProcessingTime)
                .IsRequired();

            // Define foreign key relation between Transaction and Ticket tables.
            builder.HasOne(t => t.Ticket)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
