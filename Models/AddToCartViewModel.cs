namespace MyNewProject.Models
{
    public class AddToCartViewModel
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Optional fields
        public string? ImageUrl { get; set; }  // URL of the product image
        public string? Description { get; set; }  // Product description
    }
}
