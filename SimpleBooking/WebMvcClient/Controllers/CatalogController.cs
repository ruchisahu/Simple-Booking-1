using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMvc.Services;
using WebMvc.ViewModels;
using WebMvcClient.Services;

namespace WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;

        public CatalogController(ICatalogService catalogSvc) =>
            _catalogSvc = catalogSvc;


        public async Task<IActionResult> Index(
           int? EventFilterApplied,
           int? PlaceFilterApplied, int? page)
        {
            int itemsPage = 10;
            var catalog = await
                _catalogSvc.GetCatalogItems
                (page ?? 0, itemsPage, PlaceFilterApplied,
                EventFilterApplied);
            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = catalog.Data,
                EventType=await _catalogSvc.GetCatagory(),
                Place=await _catalogSvc.GetPlace(),
                
                EventFilterApplied = EventFilterApplied ?? 0,
                PlaceFilterApplied = PlaceFilterApplied ?? 0,
                PaginationInfo = new PaginationInfo()
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsPage, //catalog.Data.Count,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling(((decimal)catalog.Count / itemsPage))
                }
            };
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return View(vm);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }

    }
}