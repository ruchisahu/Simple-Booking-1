using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using CartAPI.Domain;

namespace CartAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private ICartRepository repository;

        public TicketsController(ICartRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await repository.GetCartAsync(id);

            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]Cart value)
        {
            var basket = await repository.UpdateCartAsync(value);

            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            repository.DeleteCartAsync(id);
        }
    }
}
