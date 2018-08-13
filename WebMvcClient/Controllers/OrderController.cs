using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMvcClient.Models;
using WebMvcClient.Services;

namespace WebMvcClient.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _OrderSvc;
        public OrderController(IOrderService catalogSvc) =>

            _OrderSvc = catalogSvc;

        public IActionResult Index(string eventName,DateTime eventDate, string eventLocation, decimal eventPrice, int quantity)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.EventName = eventName;
            orderViewModel.EventDate = eventDate;
            orderViewModel.EventLocation = eventLocation;
            orderViewModel.EventPrice = eventPrice;
            orderViewModel.Quantity = quantity;

            return View(orderViewModel);
        }

        [ActionName("Index")]
        [HttpPost]
        public IActionResult Index(OrderViewModel orderViewModel)
        {
            return View(orderViewModel);
        }

    }
}