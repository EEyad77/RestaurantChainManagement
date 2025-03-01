using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models
{
    public class UserAccount
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } // In production, store a hash instead.

        // Default role for registered users is "User"
        public string Role { get; set; } = "User";
    }
}
