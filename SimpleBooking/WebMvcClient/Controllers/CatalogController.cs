using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using WebMvcClient.Models;
using WebMvcClient.Services;
using WebMvcClient.ViewModels;

namespace WebMvcClient.Controllers
{
    public class CatalogController : Controller
    {
        private IEventsSearch eventsSearchService;

        public CatalogController(IEventsSearch eventsSearchService)
        {
            this.eventsSearchService = eventsSearchService;
        }

        public async Task<IActionResult> Index(CatalogIndexViewModel model)
        {
            var viewModel = new CatalogIndexViewModel
            {
                Categories = await eventsSearchService.Categories(),
                Types = await eventsSearchService.Types(),
                Prices = await eventsSearchService.PriceType(),
                Events = await eventsSearchService.Events(model.Location, model.EventType, model.Category, model.Price)
            };
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }
    }
}
