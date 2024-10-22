namespace MyNewProject.Models
{
    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required List<OrderItem> OrderItems { get; set; } = new(); // Initialize the list
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        // Add navigation property for the Product
        public virtual Product? Product { get; set; } // Assuming you have a Product class
    }
}
