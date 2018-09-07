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
        private IEventManagementService eventManagementService;

        public CatalogController(IEventsSearch eventsSearchService, IEventManagementService evManagementService)
        {
            this.eventsSearchService = eventsSearchService;
            this.eventManagementService = evManagementService;
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

        [HttpGet]
        public async Task<IActionResult> EventDetails(int id)
        {
            var catalogEvent = await eventManagementService.GetEvent(id);
            return View(catalogEvent);
        }
    }
}
