using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantChainManagement.Data;
using RestaurantChainManagement.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantChainManagement.Controllers
{
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        public CityController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: City/Details/5 - Shows city (branch) details and upload form.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var city = await _context.Cities
                .Include(c => c.State)
                    .ThenInclude(s => s.Country)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (city == null)
                return NotFound();
            return View(city);
        }

        // POST: City/UploadPicture/5 - Handles file upload for branch picture.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPicture(int id, IFormFile picture)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
                return NotFound();

            if (picture != null && picture.Length > 0)
            {
                var extension = Path.GetExtension(picture.FileName).ToLowerInvariant();
                if (extension != ".jpg" && extension != ".jpeg")
                {
                    ModelState.AddModelError("picture", "Only JPG and JPEG images are allowed.");
                    return View("Details", city);
                }

                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }
                city.BranchPicturePath = "/uploads/" + fileName;
                _context.Update(city);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = id });
        }
    }
}
