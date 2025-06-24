using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_2_Base.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToList();
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
            var viewModel = new OrderViewModel
            {
                Customers = _context.Customers.ToList(),
                Products = _context.Products
                    .Where(p => p.Active)
                    .Select(p => new ProductSelection
                    {
                        ProductId = p.Id,
                        Name = p.Name,
                        Quantity = 0
                    }).ToList(),
                OrderDate = DateTime.Today
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OrderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Customers = _context.Customers.ToList();
                return View(viewModel);
            }

            var order = new Order
            {
                OrderDate = viewModel.OrderDate,
                CustomerId = viewModel.CustomerId
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var product in viewModel.Products.Where(p => p.Quantity > 0))
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                };
                _context.OrderProducts.Add(orderProduct);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var order = _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Products = _context.Products.ToList();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Order order, int[] selectedProductIds, int[] quantities)
        {
            if (id != order.Id) return NotFound();

            ModelState.Remove(nameof(order.Customer));
            ModelState.Remove(nameof(order.OrderProducts));

            if (ModelState.IsValid)
            {
                var existingOrder = _context.Orders
                    .Include(o => o.OrderProducts)
                    .FirstOrDefault(o => o.Id == id);

                if (existingOrder == null) return NotFound();

                existingOrder.OrderDate = order.OrderDate;
                existingOrder.CustomerId = order.CustomerId;

                // ✅ Clear + re-add producten
                existingOrder.OrderProducts.Clear();
                for (int i = 0; i < selectedProductIds.Length; i++)
                {
                    existingOrder.OrderProducts.Add(new OrderProduct
                    {
                        OrderId = id,
                        ProductId = selectedProductIds[i],
                        Quantity = quantities[i]
                    });
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Products = _context.Products.ToList();
            return View(order);
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
