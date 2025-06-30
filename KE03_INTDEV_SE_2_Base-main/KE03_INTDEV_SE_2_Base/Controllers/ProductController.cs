using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Partico_Panel.Models;

namespace Partico_Panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController(MatrixIncDbContext context) : Controller
    {
        private readonly MatrixIncDbContext _context = context;

        public IActionResult Index()
        {
            var products = _context.Products
                .Include(p => p.Parts)
                .ToList();

            return View(products);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products
                .Include(p => p.Parts)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public IActionResult Create()
        {
            var viewModel = new ProductViewModel
            {
                AllParts = _context.Parts.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllParts = _context.Parts.ToList();
                return View(vm);
            }

            var product = new Product
            {
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                Active = vm.Active
            };

            foreach (var part in _context.Parts.Where(p => vm.SelectedPartIds.Contains(p.Id)))
            {
                product.Parts.Add(part);
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products
                .Include(p => p.Parts)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var vm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Active = product.Active,
                SelectedPartIds = [.. product.Parts.Select(p => p.Id)],
                AllParts = [.. _context.Parts]
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllParts = [.. _context.Parts];
                return View(vm);
            }

            var product = _context.Products
                .Include(p => p.Parts)
                .FirstOrDefault(p => p.Id == vm.Id);
            if (product == null) return NotFound();

            product.Name = vm.Name;
            product.Description = vm.Description;
            product.Price = vm.Price;
            product.Active = vm.Active;

            product.Parts.Clear();
            foreach (var part in _context.Parts.Where(p => vm.SelectedPartIds.Contains(p.Id)))
            {
                product.Parts.Add(part);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
