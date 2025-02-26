using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models.Menu
{
    public class Product
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]

        public string Name { get; set; }

        // a description for the product (e.g., menu item details)
        public string Description { get; set; }

        // Stores the file path for the product's image
        public string ImagePath { get; set; }



        //navigation property: represents the many-to-many relationship with categories.
        public ICollection<Category> Categories { get; set; } = new List<Category>();


    }
}
