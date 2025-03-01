using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantChainManagement.Data;
using RestaurantChainManagement.Models;
using System.Threading.Tasks;

namespace RestaurantChainManagement.Controllers
{
    public class PublicCountryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PublicCountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _context.Countries
                .Include(c => c.States)
                    .ThenInclude(s => s.Cities)
                .ToListAsync();
            return View(countries);
        }

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
    }
}
