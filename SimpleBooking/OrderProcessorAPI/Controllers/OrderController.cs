using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderProcessorAPI.Data;
using OrderProcessorAPI.Domain;
using OrderProcessorAPI.Response_Entities;
using OrderProcessorAPI.ViewModels;

namespace OrderProcessorAPI.Controllers
{
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        [Route("api/[controller]/Welcome")]
        [HttpGet]
        public string Welcome()
        {
            return "Welcome to the order controller.";
        }

        [Route("api/[controller]/ProcessAnOrder")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessAnOrder(/*OrderViewModel orderView*/)
        {
            string jsonString = new StreamReader(Request.Body).ReadToEnd();
            OrderViewModel orderView = JsonConvert.DeserializeObject<OrderViewModel>(jsonString);

            User user = _orderContext.Users
                .Where(u => (u.CreditCardNo == orderView.CreditCardNo))
                .SingleOrDefault();

            if (user == null)
            {
                user = new User();

                user.Name = orderView.UserName;
                user.BillingAddress = orderView.BillingAddress;
                user.EmailAddress = orderView.EmailAddress;
                user.CreditCardNo = orderView.CreditCardNo;
                user.CSC = orderView.CSC;
                user.ExpirationDate = orderView.ExpirationDate;

                _orderContext.Users.Add(user);
            }
            //else
            //{
            //    _orderContext.Update(user);
            //}
            Event orderEvent = _orderContext.Events
                .Where(e => (e.Name == orderView.EventName) && (e.EventDate == orderView.EventDate))
                .SingleOrDefault();
            if (orderEvent == null)
            {
                orderEvent = new Event();

                orderEvent.Name = orderView.EventName;
                orderEvent.Location = orderView.Location;
                orderEvent.Price = orderView.Price;
                orderEvent.EventDate = orderView.EventDate;

                _orderContext.Events.Add(orderEvent);
            }
            //else
            //{
            //    _orderContext.Update(orderEvent);
            //}

            Ticket ticket = new Ticket();
            Transaction transaction = new Transaction();

            //Calculating totalamount
            decimal totalamount = orderView.Quantity * orderEvent.Price;

            ticket.Quantity = orderView.Quantity;
            ticket.Event = orderEvent;
            ticket.User = user;

            transaction.ProcessingTime = DateTime.Now;
            transaction.AuthCode = string.Empty;
            transaction.TotalAmount = totalamount;
            transaction.Ticket = ticket;

            user.Tickets.Add(ticket);

            orderEvent.Tickets.Add(ticket);

            ticket.Transactions.Add(transaction);

            _orderContext.Tickets.Add(ticket);

            //Planning to add an error check if user is already present in the db in next iteration


            _orderContext.Transactions.Add(transaction);

            await _orderContext.SaveChangesAsync();

            TicketForJson ticketforjson = ConvertTicketToTicketForJson(ticket);

            return Ok(ticketforjson);
        }

        [Route("api/[controller]/CancelAnOrder")]
        [HttpPost]
        public async Task<IActionResult> CancelAnOrder(int ticketId)
        {

            Transaction transaction = new Transaction();

            Ticket ticket = _orderContext.Tickets
                         .Include(t => t.Transactions)
                         .Include(t => t.User)
                         .Where(t => (t.Id == ticketId) && (t.Status == TicketStatus.Valid))
                         .SingleOrDefault();

            if (ticket == null)
            {
                return BadRequest();
            }
            Transaction purchaseTransaction = ticket.Transactions.Single();

            ticket.Status = TicketStatus.Cancelled;
            transaction.ProcessingTime = DateTime.Now;
            transaction.AuthCode = string.Empty;
            transaction.TotalAmount = -1 * purchaseTransaction.TotalAmount;
            transaction.Ticket = ticket;

            ticket.Transactions.Add(transaction);

            _orderContext.Update(ticket);
            _orderContext.Update(transaction);

            await _orderContext.SaveChangesAsync();

            return Ok();
        }

        [Route("api/[Controller]/GetTicketById")]
        [HttpGet]
        public IActionResult GetTicketById(int ticketId)
        {
            if (ticketId == 0)
            {
                return null;
            }

            Ticket ticket = _orderContext.Tickets
                            .Include(t=>t.Transactions)
                            .Include(t=>t.Event)
                            .Include(t=>t.User)
                           .Where(t => (t.Id == ticketId))
                           .Single();

            TicketForJson ticketforjson = ConvertTicketToTicketForJson(ticket);

            return Ok(ticketforjson);
        }

        private TicketForJson ConvertTicketToTicketForJson(Ticket ticket)
        {
            TicketForJson ticketForJson = new TicketForJson();
            Transaction t = ticket.Transactions.First();

            ticketForJson.AuthCode = t.AuthCode; ;
            ticketForJson.EventDateTime = ticket.Event.EventDate;
            ticketForJson.EventLocation = ticket.Event.Location;
            ticketForJson.EventName = ticket.Event.Name;

            ticketForJson.TicketId = ticket.Id;
            ticketForJson.Quantity = ticket.Quantity;
            ticketForJson.status = ticket.Status;

            ticketForJson.ProcessingTime = t.ProcessingTime;

            
            decimal TotalAmount =t.TotalAmount;
            ticketForJson.TotalAmount = TotalAmount;

            ticketForJson.UserName = ticket.User.Name;

            return ticketForJson;
        }
    }
}