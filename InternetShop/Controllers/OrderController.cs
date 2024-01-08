using Microsoft.AspNetCore.Mvc;
using InternetShop.Services.Interfaces;
using InternetShop.Models;

namespace InternetShop.Controllers
{
    public class OrderController : Controller
    {
        ILogger<OrderController> logger;
        IUserService userService;
        IOrderService orderService;
        UserAuthentication authenticationUser;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService, IUserService userService, UserAuthentication authenticationUser)
        {
            this.logger = logger; 
            this.userService = userService; 
            this.authenticationUser = authenticationUser; 
            this.orderService = orderService; 
        }

        [HttpGet]
        [Route("order/info/orderId={orderId:int}")]
        public IActionResult Info(int orderId)
        {
            // Retrieve order by ID
            Order? order = orderService.GetById(orderId);

            // Check if the user has access to this order
            if (userHasAccess(order) is false)
            {
                // Redirect to home if access is denied
                return RedirectPermanent("~/");
            }

            // Retrieve all items in the order
            List<OrderItem> items = orderService.GetAllItems(order);
            // Pass order and items to the view
            ViewBag.items = items;
            ViewBag.order = order;

            return View();
        }


        [HttpPost]
        [Route("/order/cancel/orderId={orderId:int}")]
        public IActionResult Cancel(int orderId)
        {
            Order? order = orderService.GetById(orderId);

            // Check if user has access and order is open for cancellation
            if (userHasAccess(order) is false || order.Status != OrderStatus.Open)
            {
                return RedirectPermanent("~/");
            }

            orderService.Cancel(order);

            // Redirect to the order info page
            return RedirectPermanent(String.Format("~/order/info/orderId={0}", order.Id));
        }

        [HttpPost]
        [Route("/order/complete/orderId={orderId:int}")]
        public IActionResult Complete(int orderId)
        {

            Order? order = orderService.GetById(orderId);

            // Check if user has access and order is open for completion
            if (userHasAccess(order) is false || order.Status != OrderStatus.Open)
            {
                return RedirectPermanent("~/");
            }

            // Complete the order and deduct the total sum from the user's balance
            orderService.Complete(order);
            userService.WithdrawFromBalance(authenticationUser.User, order.TotalSum);

            return RedirectPermanent(String.Format("~/order/info/orderId={0}", order.Id));
        }

        private bool userHasAccess(Order? order)
        {
            // User must be authenticated, order must exist, and user must be the owner of the order
            return order != null && authenticationUser.IsAuthenticated() && order.UserId == authenticationUser.User.Id;
        }
    }
}
