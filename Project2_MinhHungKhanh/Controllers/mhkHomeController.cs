using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project2_MinhHungKhanh.Models;

namespace Project2_MinhHungKhanh.Controllers
{
    public class mhkHomeController : Controller
    {
        private readonly ILogger<mhkHomeController> _logger;

        public mhkHomeController(ILogger<mhkHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult mhkIndex()
        {
            return View();
        }

        public IActionResult mhkAbout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult mhkError()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
