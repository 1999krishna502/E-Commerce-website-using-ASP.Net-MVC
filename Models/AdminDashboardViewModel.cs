using System.Collections.Generic;

namespace MyNewProject.Models
{
    public class AdminDashboardViewModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }

        // List to hold products
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
