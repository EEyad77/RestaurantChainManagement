using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models
{
    public class Country
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        public string Name { get; set; }

        // Path or URL for the country flag (file upload by admin)
        public string FlagPath { get; set; }

        // country has many states 
        public ICollection<State> States { get; set; } = new List<State>();


    }
}