using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models.Locations
{
    public class Country
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Country name is required")]
        public string Name { get; set; }

        public string FlagPath { get; set; }

        //Navigation property: country can have many states 
        public ICollection<State> States { get; set; } = new List<State>();


    }
}
