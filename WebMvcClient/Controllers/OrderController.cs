using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebMvcClient.Models;
using WebMvcClient.Services;

namespace WebMvcClient.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderSvc;

        public OrderController(IOrderService orderSvc) =>

            _orderSvc = orderSvc;
        [HttpGet]
        public IActionResult Index(string eventName,DateTime eventDate, string eventLocation, decimal eventPrice, int quantity)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            orderViewModel.EventName = eventName;
            orderViewModel.EventDate = eventDate;
            orderViewModel.Location = eventLocation;
            orderViewModel.Price = eventPrice;
            orderViewModel.Quantity = quantity;
            orderViewModel.TotalAmount = quantity*eventPrice;

            return View(orderViewModel);
        }

        [ActionName("Index")]
        [HttpPost]
        public async Task<IActionResult> Index(OrderViewModel orderViewModel)
        {
            Ticket ticket = await _orderSvc.ProcessAnOrder(orderViewModel);
            string ticketSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

            TempData["TicketDetails"] = ticketSerialized;
            return RedirectToAction("Index", "Ticket");

            //string testResult = await _orderSvc.TestWelcome();
            //return View(testResult);
        }

    }
}