using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebMvcClient.Models;

namespace WebMvcClient.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        //public IActionResult Index(string eventName,DateTime eventDateTime,string location,string userName,int ticketId,int status,int Quantity,int TotalAmount,int Authcode,DateTime ProcessingTime)
        public IActionResult Index()
        {
            string serializedTicket = (string)TempData["TicketDetails"];
            Ticket t = JsonConvert.DeserializeObject<Ticket>(serializedTicket);
            
            //Ticket t1 = (Ticket)
            return View(t);
        }
    }
}