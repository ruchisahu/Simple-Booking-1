using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> Index(string category, string eventType, string price, string location, int? page)
        {
            const int itemsPage = 6;

            if(location == null)
            {
                location = "Bellevue";
            }

            var actualPage = page.HasValue ? page.Value : 0;
            var events = await eventsSearchService.Events(location, eventType, category, price, actualPage, itemsPage);

            events.Data.ForEach(e => e.DisplayPrice = e.PriceType == EventPriceType.Free ? "Free" : e.Price.ToString());

            var viewModel = new CatalogIndexViewModel
            {
                Categories = await eventsSearchService.Categories(),
                Types = await eventsSearchService.Types(),
                Prices = await eventsSearchService.PriceType(),
                Events = events.Data,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage, //catalog.Data.Count,
                    TotalItems = events.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)events.Count / itemsPage))
                },
                Location = location,
                Category = category,
                EventType = eventType,
                Price = price
            };

            viewModel.PaginationInfo.Next = (viewModel.PaginationInfo.ActualPage == viewModel.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            viewModel.PaginationInfo.Previous = (viewModel.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

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
