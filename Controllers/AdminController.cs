using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNewProject.Models;
using System.IO;
using System.Threading.Tasks;

namespace MyNewProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public AdminController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        // Admin Dashboard
        public async Task<IActionResult> Admin()
        {
            var model = new AdminDashboardViewModel
            {
                UserName = "AdminUser",
                Email = "admin@example.com",
                Products = await _context.Products.ToListAsync() // Fetch from database
            };

            return View(model);
        }

        // Show the product upload form
        public IActionResult UploadProduct()
        {
            return View();
        }

        // Handle product upload (POST)
        [HttpPost]
        public async Task<IActionResult> UploadProduct(Product model, IFormFile productImage)
        {
            if (ModelState.IsValid)
            {
                // Handle the image file
                if (productImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder); // Ensure directory exists

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + productImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await productImage.CopyToAsync(fileStream);
                    }

                    model.ImagePath = "/images/" + uniqueFileName; // Save the relative image path
                }

                // Save product to the database
                _context.Products.Add(model);
                await _context.SaveChangesAsync();

                // Redirect back to the dashboard after saving
                return RedirectToAction("Admin");
            }

            return View(model);
        }

    }
}
