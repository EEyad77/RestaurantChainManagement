using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantChainManagement.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        public string CountryName { get; set; }

        public string FlagPath { get; set; }

        public List<StateViewModel> States { get; set; } = new List<StateViewModel>();
    }

    public class StateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "State name is required")]
        public string StateName { get; set; }

        public List<CityViewModel> Cities { get; set; } = new List<CityViewModel>();
    }

    public class CityViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City name is required")]
        public string CityName { get; set; }
    }
}
