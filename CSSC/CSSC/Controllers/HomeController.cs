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

        /// <summary>
        /// Retorna a visualização dos serviços de acordo com o papel do utilizador.
        /// </summary>
        /// <returns>
        /// Um <see cref="Task"/> que representa a operação assíncrona. O resultado contém um <see cref="IActionResult"/>.
        /// A visualização dos serviços é retornada se o utilizador estiver autenticado como operador ou padrão.
        /// Redireciona para a página de login se o utilizador não estiver autenticado.
        /// </returns>
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

        /// <summary>
        /// Retorna a visualização da política de privacidade.
        /// </summary>
        /// <returns>
        /// Um <see cref="IActionResult"/> que representa a visualização da política de privacidade.
        /// </returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Retorna a visualização de erro.
        /// </summary>
        /// <returns>
        /// Um <see cref="IActionResult"/> que representa a visualização de erro.
        /// </returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
