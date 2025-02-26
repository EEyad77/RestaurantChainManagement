using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models.Locations
{
    public class State
    {

        public int Id { get; set; }


        [Required(ErrorMessage ="State name is required")]
        public string Name { get; set; }


        //foreign key: link/connect each state with its country 
        [Required]
        public int CountryId { get; set; }


        //navigation property: each state belongs to a country
        public Country Country { get; set; }


        //navigation property: state can have many cities
        public ICollection<City> Cities { get; set; } = new List<City>();

    }
}
