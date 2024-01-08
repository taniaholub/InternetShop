using Microsoft.AspNetCore.Mvc;
using InternetShop.Services.Interfaces;
using InternetShop.Models;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        // Fields for dependency services
        private IOrderService orderService;
        private ILogger<HomeController> logger;
        private AbstractAppDbContext db;
        private UserAuthentication authenticationUser;

        // Constructor for HomeController with dependency injection
        public HomeController(IOrderService userService, UserAuthentication authenticationUser, ILogger<HomeController> logger, AbstractAppDbContext db)
        {
            this.orderService = userService; // Order service used to handle order-related logic
            this.authenticationUser = authenticationUser; // Service for user authentication
            this.logger = logger; // Logger service for logging
            this.db = db; // Database context for database operations
        }

        // HTTP GET method for the root URL ("/")
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            if (authenticationUser.IsAuthenticated())
            {
                // If authenticated, retrieve all orders for the user and pass them to the view
                ViewBag.orders = orderService.GetAllOrders(authenticationUser.User);
                // Pass the authenticated user information to the view
                ViewBag.user = authenticationUser.User;
                return View();
            }
            // If not authenticated, redirect the user to the login page
            return RedirectPermanent("~/user/login");
        }
    }
}
