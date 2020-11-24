using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptimeService ;

        public HomeController(UptimeService service)
        {
            uptimeService = service;
        }

        public IActionResult Index() => View(
            new Dictionary<string, string> { 
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptimeService.Uptime}ms"
            });
    }
}
