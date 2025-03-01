using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models
{
    public class State
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "State name is required")]
        public string Name { get; set; }


        //foreign key: link/connect each state with its country 
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }


        //state has many cities
        public ICollection<City> Cities { get; set; } = new List<City>();

    }
}