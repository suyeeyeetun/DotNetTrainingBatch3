using DotNetTrainingBatch3.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DotNetTrainingBatch3.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["PageTitle"] = "Our Products";
            ViewData["AvailableProducts"] = new List<string> { "Laptop", "Mouse", "Keyboard" };
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
