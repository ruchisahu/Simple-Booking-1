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
        //private IOrderService _OrderSvc;
        //public OrderController(IOrderService orderSvc) =>

        //    _OrderSvc = orderSvc;
        [HttpGet]
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
            Ticket ticket = new Ticket();
            ticket.EventName = "FitFest";
            ticket.EventLocation = "Redmond Town Center";
            //Ticket ticket = await _OrderSvc.ProcessAnOrder(orderViewModel);
            string ticketSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

            TempData["TicketDetails"] = ticketSerialized;
            return RedirectToAction("Index", "Ticket");
            //return View(orderViewModel);
        }

    }
}