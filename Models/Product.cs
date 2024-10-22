namespace MyNewProject.Models
{
    public class Product
    {
        public int Id { get; set; }  // Primary key
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;  // Path to the image
    }
}
