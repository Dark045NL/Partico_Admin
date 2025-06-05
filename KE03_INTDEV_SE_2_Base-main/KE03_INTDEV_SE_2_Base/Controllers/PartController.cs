using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public PartController(MatrixIncDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Parts.ToList());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            if (part == null) return NotFound();
            return View(part);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Part part)
        {
            if (ModelState.IsValid)
            {
                _context.Parts.Add(part);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var part = _context.Parts.Find(id);
            if (part == null) return NotFound();
            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Part part)
        {
            if (id != part.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(part);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Parts.Any(p => p.Id == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(part);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            if (part == null) return NotFound();
            return View(part);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var part = _context.Parts.Find(id);
            if (part != null)
            {
                _context.Parts.Remove(part);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
