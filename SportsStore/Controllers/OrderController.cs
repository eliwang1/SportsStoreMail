using EmailService;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Text;

namespace SportsStore.Controllers {

    public class OrderController : Controller {
        private IOrderRepository repository;
        private Cart cart;
        private readonly IEmailSender _emailSender;

        public OrderController(IOrderRepository repoService, Cart cartService, IEmailSender emailSender) {
            repository = repoService;
            cart = cartService;
            _emailSender = emailSender;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid) {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                StringBuilder body = new StringBuilder();
                body.AppendLine(order.Name).AppendLine(order.Line1);
                body.Append("<br>");
                body.AppendLine("<br>");
                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}", line.Quantity,
                                      line.Product.Name,
                                      subtotal);
                }
                var message = new Message(new string[] { "xxxxxxx@xxxxx.com" }, "New order submitted!", body.ToString(), null);
                _emailSender.SendEmail(message);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            } else {
                return View();
            }
        }
    }
}
