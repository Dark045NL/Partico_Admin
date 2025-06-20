using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatsController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public StatsController(MatrixIncDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get orders per customer
            var ordersPerCustomer = await _context.Customers
                .Select(c => new
                {
                    CustomerName = c.Name,
                    OrderCount = c.Orders.Count
                })
                .ToListAsync();

            // Get product order statistics
            var productStats = await _context.Products
                .Select(p => new
                {
                    ProductName = p.Name,
                    OrderCount = p.Orders.Count,
                    Price = p.Price
                })
                .OrderByDescending(p => p.OrderCount)
                .ToListAsync();

            ViewBag.OrdersPerCustomer = ordersPerCustomer;
            ViewBag.ProductStats = productStats;

            return View();
        }
    }
}