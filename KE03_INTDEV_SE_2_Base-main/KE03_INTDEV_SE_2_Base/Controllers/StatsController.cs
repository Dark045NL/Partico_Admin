using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Partico_Panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatsController(MatrixIncDbContext context) : Controller
    {
        private readonly MatrixIncDbContext _context = context;

        public async Task<IActionResult> Index()
        {
            // Bestellingen per klant
            var ordersPerCustomer = await _context.Customers
                .Select(c => new
                {
                    customerName = c.Name,
                    orderCount = c.Orders.Count
                })
                .ToListAsync();

            // Aantal bestellingen per product + omzet (aantal * prijs)
            var productStats = await _context.Products
                .Select(p => new
                {
                     ProductName = p.Name,
                        OrderCount = p.OrderProducts.Sum(op => (int?)op.Quantity) ?? 0,
                        p.Price,
                        Revenue = (p.OrderProducts.Sum(op => (int?)op.Quantity) ?? 0) * p.Price
                })
                .OrderByDescending(p => p.OrderCount)
                .ToListAsync();

            // Omzet per maand (optioneel dummy)
            var monthlyRevenue = new[] { 12500, 15000, 11000, 13500, 16500, 18000 };

            ViewBag.OrdersPerCustomer = ordersPerCustomer;
            ViewBag.ProductStats = productStats;
            ViewBag.MonthlyRevenue = monthlyRevenue;

            return View();
        }
    }
}
