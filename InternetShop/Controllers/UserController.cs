using Microsoft.AspNetCore.Mvc;
using InternetShop.Services.Interfaces;
using InternetShop.Models;

namespace InternetShop.Controllers
{
    public class UserController : Controller
    {
        UserAuthentication authenticationUser;
        IUserService userService;
        ILogger<UserController> logger;

        public UserController(UserAuthentication authenticationUser, IUserService userService, ILogger<UserController> logger)
        {
            this.authenticationUser = authenticationUser; 
            this.userService = userService; 
            this.logger = logger; 
        }

        [HttpGet]
        [Route("/user/register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/user/register")]
        public IActionResult Register(User user)
        {
            // Checks if the provided user data is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Tries to register the user
            User? registeredUser = userService.SignUp(user);

            if (registeredUser == null)
            {
                TempData["AlertMessage"] = "This username is already taken, please choose another";
            }
            else
            {
                TempData["AlertMessage"] = "You have successfully signed up!";
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("/user/login")]
        public IActionResult Login()
        {
            // Redirects authenticated users to home page
            if (authenticationUser.IsAuthenticated())
            {
                return RedirectPermanent("~/");
            }

            return View();
        }

        [HttpPost]
        [Route("/user/login")]
        public IActionResult Login(string name, string password)
        {
            // Check if the login credentials are provided
            if (name is null || password is null)
            {
                return BadRequest();
            }

            // Tries to log in the user
            bool isLoggedIn = userService.LogIn(name, password);

            // Check if login was successful
            if (isLoggedIn)
            {
                return RedirectPermanent("~/");
            }

            TempData["AlertMessage"] = "Password or Name is not valid";
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("/user/logout")]
        public IActionResult Logout()
        {
            // If the user is authenticated, log them out
            if (authenticationUser.IsAuthenticated())
            {
                TempData["AlertMessage"] = "You have successfully logged out!";
                userService.LogOut();
            }
            
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Route("user/addToBalance")]
        public IActionResult AddToBalance(double amount)
        {
            // If the user is not authenticated, redirect to login
            if (authenticationUser.IsNotAuthenticated())
            {
                TempData["AlertMessage"] = "You must log in!";
                return RedirectToAction("Login");
            }

            // If the amount is valid, add it to the user's balance
            if (amount > 0)
            {
                userService.AddToBalance(authenticationUser.User, amount);
            }

            return RedirectPermanent("~/");
        }
    }
}
