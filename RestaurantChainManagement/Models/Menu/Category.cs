using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models.Menu
{
    public class Category
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Category name is required")]
        public string Name { get; set; }

        // a description for the category
        public string Description { get; set; }


        // Navigation property: many-to-many relationship with Product. 
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
