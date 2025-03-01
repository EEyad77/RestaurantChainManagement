using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantChainManagement.Data;
using RestaurantChainManagement.Models;
using RestaurantChainManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantChainManagement.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminCountryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminCountryController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: AdminCountry
        public async Task<IActionResult> Index()
        {
            var countries = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .ToListAsync();
            return View(countries);
        }

        // GET: AdminCountry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var country = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
                return NotFound();
            return View(country);
        }

        // GET: AdminCountry/Create
        public IActionResult Create()
        {
            var vm = new CountryViewModel();
            // Initialize with one state containing one city by default
            vm.States.Add(new StateViewModel { Cities = new List<CityViewModel> { new CityViewModel() } });
            return View(vm);
        }

        // POST: AdminCountry/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CountryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var country = new Country
                {
                    Name = vm.CountryName,
                    FlagPath = vm.FlagPath
                };
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();

                foreach (var stateVm in vm.States)
                {
                    var state = new State
                    {
                        Name = stateVm.StateName,
                        CountryId = country.Id
                    };
                    _context.States.Add(state);
                    await _context.SaveChangesAsync();

                    foreach (var cityVm in stateVm.Cities)
                    {
                        var city = new City
                        {
                            Name = cityVm.CityName,
                            StateId = state.Id,
                            BranchPicturePath = string.Empty
                        };
                        _context.Cities.Add(city);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: AdminCountry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var country = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
                return NotFound();

            var vm = new CountryViewModel
            {
                Id = country.Id,
                CountryName = country.Name,
                FlagPath = country.FlagPath,
                States = country.States.Select(s => new StateViewModel
                {
                    Id = s.Id,
                    StateName = s.Name,
                    Cities = s.Cities.Select(ci => new CityViewModel
                    {
                        Id = ci.Id,
                        CityName = ci.Name
                    }).ToList()
                }).ToList()
            };
            return View(vm);
        }

        // POST: AdminCountry/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CountryViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var country = await _context.Countries
                    .Include(c => c.States)
                        .ThenInclude(s => s.Cities)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (country == null)
                    return NotFound();

                // Update country fields
                country.Name = vm.CountryName;
                country.FlagPath = vm.FlagPath;

                // Remove all existing nested states and cities (for simplicity)
                foreach (var state in country.States.ToList())
                {
                    foreach (var city in state.Cities.ToList())
                    {
                        _context.Cities.Remove(city);
                    }
                    _context.States.Remove(state);
                }
                await _context.SaveChangesAsync();

                // Add new nested states and cities from the view model
                foreach (var stateVm in vm.States)
                {
                    var state = new State
                    {
                        Name = stateVm.StateName,
                        CountryId = country.Id
                    };
                    _context.States.Add(state);
                    await _context.SaveChangesAsync();

                    foreach (var cityVm in stateVm.Cities)
                    {
                        var city = new City
                        {
                            Name = cityVm.CityName,
                            StateId = state.Id
                        };
                        _context.Cities.Add(city);
                    }
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: AdminCountry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country == null)
                return NotFound();
            return View(country);
        }

        // POST: AdminCountry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (country != null)
            {
                foreach (var state in country.States.ToList())
                {
                    foreach (var city in state.Cities.ToList())
                    {
                        _context.Cities.Remove(city);
                    }
                    _context.States.Remove(state);
                }
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
