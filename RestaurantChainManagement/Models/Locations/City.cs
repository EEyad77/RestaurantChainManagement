using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.Models.Locations
{
    public class City
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="City name is required")]
        public string Name { get; set; }

        //foreign key: link each city with its state 
        [Required]
        public int StateId { get; set; }

        //navigation property: each city belongs to a state 
        public State State { get; set; }

     

    }
}
