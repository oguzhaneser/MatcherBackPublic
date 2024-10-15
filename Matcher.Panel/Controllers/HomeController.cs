using Matcher.Panel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Matcher.Panel.Controllers
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
            string user = HttpContext.User.Identity.Name;

            //var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.SerialNumber).Value;
            _logger.Log(LogLevel.Information, user + " anasayfaya geldi.");

            return View(model: user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string errorMessage = "Beklenmeyen bir hata oluştu.")
        {
            return View(model: errorMessage);
        }
    }
}