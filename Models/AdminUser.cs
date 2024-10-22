namespace MyNewProject.Models
{
    public class AdminUser
    {
        public int Id { get; set; } // Primary key
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
