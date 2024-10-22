using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyNewProject.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace MyNewProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var products = await _context.Products.ToListAsync();

#pragma warning disable CS8601 // Possible null reference assignment.
            var model = new DashboardViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AddressLine1 = user.AddressLine1,
                AddressLine2 = user.AddressLine2,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Country = user.Country,
                Products = products
            };
#pragma warning restore CS8601 // Possible null reference assignment.

            return View(model);
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // [HttpGet] AddToCart method - Get product info and display to user
        [HttpGet]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            var model = new AddToCartViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price
            };

            return View(model);
        }

        // [HttpPost] AddToCart method - Add product to cart
         [HttpPost]
public IActionResult AddToCart(int productId, int quantity)
{
    // Retrieve product from database based on productId
    var product = _context.Products.FirstOrDefault(p => p.Id == productId);

    if (product == null)
    {
        // Handle case where product is not found
        return NotFound();
    }

    // Create a new cart item
    var cartItem = new CartItem
    {
        ProductId = product.Id,
        ProductName = product.ProductName,
        Price = product.Price,
        Quantity = quantity
    };

    // Retrieve cart from session (or initialize a new one if it doesn’t exist)
    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

    // Add item to cart
    cart.Add(cartItem);

    // Save cart back to session
    HttpContext.Session.SetObjectAsJson("Cart", cart);

    // Redirect to cart page
    return RedirectToAction("Cart");
}


        // Display the cart page
        public IActionResult Cart()
{
    // Retrieve the cart from session using the custom extension method
    var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

    return View(cartItems); // Pass the list of cart items to the view
}
[HttpPost]
public async Task<IActionResult> BuyNow(int productId, int quantity)
{
    // Retrieve the product from the database
    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

    if (product == null)
    {
        return NotFound();
    }

    // Create a new order item
    var orderItem = new OrderItemViewModel
    {
        ProductId = product.Id,
        ProductName = product.ProductName,
        Price = product.Price,
        Quantity = quantity
    };

            // Create a new order (you need to create an Order entity)
#pragma warning disable CS8601 // Possible null reference assignment.
            var order = new Order
    {
        UserId = _userManager.GetUserId(User),
        OrderItems = new List<OrderItem> // Assuming you have an OrderItem model
        {
            new OrderItem
            {
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            }
        },
        TotalAmount = orderItem.Price * quantity,
        OrderDate = DateTime.UtcNow
    };
#pragma warning restore CS8601 // Possible null reference assignment.

            // Save the order to the database
            _context.Orders.Add(order);
    await _context.SaveChangesAsync();

    // Redirect to a confirmation page
    return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
}
public async Task<IActionResult> OrderConfirmation(int orderId)
{
    var order = await _context.Orders
        .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product) // Load the Product details
        .FirstOrDefaultAsync(o => o.Id == orderId);

    if (order == null)
    {
        return NotFound();
    }

    return View(order);
}


    }
}
