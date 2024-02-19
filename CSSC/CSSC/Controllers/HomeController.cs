using CSSC.Areas.Identity.Data;
using CSSC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace CSSC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CSSCContext _context;

       

        public HomeController(ILogger<HomeController> logger, CSSCContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Operador"))
            {
                var services = _context.ServiceModel
                    .Include(r => r.csscUser)
                    .ToList();
                return View(services);
            }
            else if (User.IsInRole("Default"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userServices = await _context.ServiceModel
                    .Include(r => r.csscUser)
                    .Where(r => r.ServIdUtilizador == Guid.Parse(userId)).ToListAsync();
                return View(userServices);
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
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
