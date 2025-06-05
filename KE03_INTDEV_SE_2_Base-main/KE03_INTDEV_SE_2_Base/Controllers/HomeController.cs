using System.Diagnostics;
using KE03_INTDEV_SE_2_Base.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Alleen toegankelijk voor ingelogde gebruikers met rol "Admin"
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        // Ook alleen toegankelijk voor Admins
        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        // Foutpagina mag publiek toegankelijk blijven
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
