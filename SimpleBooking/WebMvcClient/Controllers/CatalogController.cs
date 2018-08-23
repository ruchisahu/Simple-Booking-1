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
        public async Task<IActionResult> Index(CatalogIndexViewModel model)
        {
            var catalogService = new CatalogService();
            var viewModel = new CatalogIndexViewModel
            {
                Categories = await catalogService.Categories(),
                Types = await catalogService.Types(),
                Prices = await catalogService.PriceType(),
                Events = await catalogService.Events(model.Location, model.EventType, model.Category, model.Price)
            };
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public IActionResult Login()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }
    }
}
