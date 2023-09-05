using GoodBooksMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoodBooksMvc.Controllers
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
              // Get the count from the session state and initialize to 0
            int count = HttpContext.Session.GetInt32("visitCount") ?? 0;

              // Increment the count
            count += 1;

              // Store the count back into the session state
            HttpContext.Session.SetInt32("visitCount", count);

              // Pass the count to the view
            ViewBag.VisitCount = count;

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