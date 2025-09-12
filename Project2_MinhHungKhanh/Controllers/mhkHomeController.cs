using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project2_MinhHungKhanh.Models;

namespace Project2_MinhHungKhanh.Controllers
{
    public class MhkHomeController : Controller
    {
        private readonly ILogger<MhkHomeController> _logger;

        public MhkHomeController(ILogger<MhkHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult MhkIndex()
        {
            return View();
        }

        public IActionResult MhkAbout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult MhkError()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
