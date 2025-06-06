using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly MatrixIncDbContext _context;

        public OrderController(IOrderRepository orderRepo, MatrixIncDbContext context)
        {
            _orderRepo = orderRepo;
            _context = context;
        }

        public IActionResult Index()
        {
            var orders = _orderRepo.GetAllOrders();
            return View(orders);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var order = _orderRepo.GetOrderById(id.Value);
            if (order == null) return NotFound();

            return View(order);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {

            ModelState.Remove(nameof(order.Customer));
            ModelState.Remove(nameof(order.Products));

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("ModelState Error: " + error.ErrorMessage);
                }

                ViewBag.Customers = _context.Customers.ToList();
                return View(order);
            }

            _orderRepo.AddOrder(order);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = _orderRepo.GetOrderById(id.Value);
            if (order == null) return NotFound();

            ViewBag.Customers = _context.Customers.ToList();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.Id) return NotFound();

            ModelState.Remove(nameof(order.Customer));
            ModelState.Remove(nameof(order.Products));

            if (!ModelState.IsValid)
            {
                ViewBag.Customers = _context.Customers.ToList();
                return View(order);
            }

            _orderRepo.UpdateOrder(order);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var order = _orderRepo.GetOrderById(id.Value);
            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _orderRepo.GetOrderById(id);
            if (order != null)
            {
                _orderRepo.DeleteOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
