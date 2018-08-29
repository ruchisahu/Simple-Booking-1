using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.CircuitBreaker;
using Stripe;
using WebMvcClient.Models;
using WebMvcClient.Services;

namespace WebMvcClient.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderSvc;
        private readonly IIdentityService<ApplicationUser> _identitySvc;
        private readonly ILogger<OrderController> _logger;
        private readonly PaymentSettings paymentSettings;

        public OrderController(IOptions<PaymentSettings> paymentSettings, IOrderService orderSvc, IIdentityService<ApplicationUser> identitySvc)
        {
            _identitySvc = identitySvc;
            _orderSvc = orderSvc;
            this.paymentSettings = paymentSettings.Value;
        }
        [HttpGet]
        public IActionResult Index(string eventName,DateTime eventDate, string eventLocation, decimal eventPrice, int quantity)
        {
            var user = _identitySvc.Get(HttpContext.User);
            //var cart = await _cartSvc.GetCart(user);
            //var order = _cartSvc.MapCartToOrder(cart);
            ViewBag.StripePublishableKey = paymentSettings.StripePublicKey;
            //return View(order);

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
            //if (ModelState.IsValid)
            {
                var user = _identitySvc.Get(HttpContext.User);
                OrderViewModel order = orderViewModel;
                order.BuyerId = user.Id;
                order.EmailAddress = user.Email;

                var chargeOptions = new StripeChargeCreateOptions()
                {
                    //required
                    Amount = (int)(order.TotalAmount * 100),
                    Currency = "usd",
                    SourceTokenOrExistingSourceId = order.StripeToken,
                    //optional
                    Description = string.Format("Order Payment {0}", order.UserName),
                    ReceiptEmail = order.EmailAddress
                };
                var chargeService = new StripeChargeService();
                chargeService.ApiKey = paymentSettings.StripePrivateKey;
                StripeCharge stripeCharge = null;
                try
                {
                    stripeCharge = chargeService.Create(chargeOptions);
                    //_logger.LogDebug("Stripe charge object creation" + stripeCharge.StripeResponse.ObjectJson);
                }
                catch (StripeException stripeException)
                {
                    //_logger.LogDebug("Stripe exception " + stripeException.Message);
                    ModelState.AddModelError(string.Empty, stripeException.Message);
                    return View(orderViewModel);
                }
                try
                {
                    if (stripeCharge.Id != null)
                    {
                        //_logger.LogDebug("TransferID :" + stripeCharge.Id);
                        order.AuthCode = stripeCharge.Id;

                        //_logger.LogDebug("User {userName} started order processing", user.UserName);
                        Ticket ticket = await _orderSvc.ProcessAnOrder(orderViewModel);
                        //_logger.LogDebug("User {userName} finished order processing  of {orderId}.", order.UserName, order.OrderId);

                        //await _cartSvc.ClearCart(user);
                        string ticketSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(ticket);

                        TempData["TicketDetails"] = ticketSerialized;
                        return RedirectToAction("Index", "Ticket");
                    }
                    else
                    {
                        ViewData["message"] = "Payment cannot be processed, try again";
                        return View(orderViewModel);
                    }

                }
                catch (BrokenCircuitException)
                {
                    ModelState.AddModelError("Error", "It was not possible to create a new order, please try later on. (Business Msg Due to Circuit-Breaker)");
                    return View(orderViewModel);
                }
            }
            //else
            //{
            //    return View(orderViewModel);
            //}
        }
        //public IActionResult Complete(int id, string userName)
        //{
        //    _logger.LogInformation("User {userName} completed checkout on order {orderId}.", userName, id);
        //    return View(id);
        //}
    }
}