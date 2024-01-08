using Microsoft.AspNetCore.Mvc;
using InternetShop.Models;
using InternetShop.Services.Interfaces;

namespace InternetShop.Controllers
{
    public class ProductController : Controller
    {
        ILogger<ProductController> logger;
        IProductService productService;
        IOrderService orderService;
        UserAuthentication authenticationUser;

        public ProductController(ILogger<ProductController> logger, IProductService productService,
                                 IOrderService orderService, UserAuthentication authenticationUser)
        {
            this.logger = logger; 
            this.productService = productService; 
            this.orderService = orderService; 
            this.authenticationUser = authenticationUser; 
        }

        [HttpGet]
        [Route("/product/all")]
        public IActionResult GetProductsList()
        {
            // Fetch all products and pass them to the view
            ViewBag.products = productService.GetAll();
            return View();
        }

        [HttpGet]
        [Route("/product/get/one/info/{productId:int}")]
        public IActionResult GetProductById(int productId)
        {
            Product? product = productService.GetById(productId);

            // Check if the user has access to view the product
            if (userHasAccess(product) is false)
            {
                return RedirectPermanent("~/");
            }

            ViewBag.product = product;
            return View();
        }

        [HttpPost]
        [Route("/product/one/order/")]
        public IActionResult OrderProduct(int productId, int quantity)
        {
            Product? product = productService.GetById(productId);

            // Check if the user has access and if the quantity is valid
            if (userHasAccess(product) is false || quantity < 1)
            {
                return RedirectPermanent("~/");
            }


            bool isOrdered = orderService.Order(product, quantity);
            if (isOrdered is false)
            {
                TempData["AlertMessage"] = "There are insufficient funds in the account";
            }
            else
            {
                TempData["AlertMessage"] = "The product has been successfully added to the cart";
            }

            return RedirectPermanent(String.Format("/product/get/one/info/{0}", productId));
        }

        private bool userHasAccess(Product? product)
        {
            // The user must be authenticated to view a product
            return product != null && authenticationUser.IsAuthenticated();
        }
    }
}
