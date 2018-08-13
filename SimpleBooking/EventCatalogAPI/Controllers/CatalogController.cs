using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Catalog")]
    public class CatalogController : Controller
    {
        private readonly EventCatalogContext _catalogContext;

        public CatalogController(EventCatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Catalogs()
        {
            var items = await _catalogContext.Eventcatalogs.ToListAsync();
            //   var items = 6;
            return Ok(items);


        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Users()
        {
            var items = await _catalogContext.Users.ToListAsync();
            //   var items = 6;
            return Ok(items);


        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Tickets()
        {
            var items = await _catalogContext.Tickets.ToListAsync();
            //   var items = 6;
            return Ok(items);


        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Places()
        {
            var items = await _catalogContext.Places.ToListAsync();
            //   var items = 6;
            return Ok(items);


        }

        [HttpPost]
        [Route("Ticket")]
        public async Task<IActionResult> CreateTicketAsync([FromBody] Ticket Ticket)
        {
            var item = new Ticket
            {
                TicketId = Ticket.TicketId,
                EventId=Ticket.EventId


            };
            _catalogContext.Tickets.Add(item);
            await _catalogContext.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost]
        [Route("Event")]
        public async Task<IActionResult> CreateEventAsync([FromBody] EventCatalog Event)
        {
            var item = new EventCatalog
            {
                Id = Event.Id,
                Name = Event.Name,
                Description = Event.Description,
                StartDate = Event.StartDate,
                EndDate = Event.EndDate,
                Price = Event.Price,
                ImageURL = Event.ImageURL,
                PriceType = Event.PriceType,
                Category = Event.Category,
                Type = Event.Type
            };

            _catalogContext.Eventcatalogs.Add(item);
            await _catalogContext.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> CreateUserAsync([FromBody] User User)
        {
            var item = new User
            {
                UserId = User.UserId,
                Name = User.Name,
                Password = User.Password,
                Email = User.Email,
                BillingAddress = User.BillingAddress,
               
                CreditCardNo = User.CreditCardNo,
                EventId = User.EventId,


            };
            _catalogContext.Users.Add(item);
            await _catalogContext.SaveChangesAsync();

            return Ok(item);
        }
        [HttpPost]
        [Route("Place")]
        public async Task<IActionResult> CreatePlaceAsync([FromBody] Place Place)
        {
            var item = new Place
            {
                PlaceId = Place.PlaceId,
                PlaceName = Place.PlaceName,
                Address = Place.Address,
                City = Place.City,
                State = Place.State,
                ZipCode = Place.ZipCode,
                PlacePrice = Place.PlacePrice,


            };
            _catalogContext.Places.Add(item);
            await _catalogContext.SaveChangesAsync();

            return Ok(item);
        }
    }
}




