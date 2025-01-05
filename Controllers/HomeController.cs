using System.Diagnostics;
using FinalProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        
            private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger)
            {
                _logger = logger;
            }

            [Authorize]
            public IActionResult Index()
            {
                var passengers = GetPassengerCount(); // Simulate fetching passenger count
                ViewBag.PassengerCount = passengers;
                return View();
            }

            private int GetPassengerCount()
            {
                // Simulate fetching passenger count from a database
                return 42; // Example number
            }

            public IActionResult Start()
            {
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

