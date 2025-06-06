using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KE03_INTDEV_SE_2_Base.Models;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class SearchController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public SearchController(MatrixIncDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Json(new { results = new object[] { } });

            query = query.ToLower();
            var results = new List<SearchResult>();

            // Search in Products
            var products = await _context.Products
                .Where(p => p.Name.ToLower().Contains(query))
                .Take(5)
                .Select(p => new SearchResult
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Type = "Product",
                    Url = Url.Action("Details", "Product", new { id = p.Id })
                })
                .ToListAsync();
            results.AddRange(products);

            // Search in Customers (Klanten)
            var customers = await _context.Customers
                .Where(c => c.Name.ToLower().Contains(query))
                .Take(5)
                .Select(c => new SearchResult
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    Type = "Klant",
                    Url = Url.Action("Details", "Customer", new { id = c.Id })
                })
                .ToListAsync();
            results.AddRange(customers);

            // Search in Parts (Onderdelen)
            var parts = await _context.Parts
                .Where(p => p.Name.ToLower().Contains(query))
                .Take(5)
                .Select(p => new SearchResult
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    Type = "Onderdeel",
                    Url = Url.Action("Details", "Part", new { id = p.Id })
                })
                .ToListAsync();
            results.AddRange(parts);

            return Json(new { results });
        }
    }
} 