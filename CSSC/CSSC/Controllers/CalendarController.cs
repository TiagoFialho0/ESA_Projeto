using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSSC.Areas.Identity.Data;
using CSSC.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class CalendarController : Controller
{
    private readonly CSSCContext _context;

    public CalendarController(CSSCContext context)
    {
        _context = context;
    }

    public async Task<ActionResult> Index()
    {
        var months = GetMonthsBetween(DateTime.Today, DateTime.Today.AddMonths(11));

        if (User.IsInRole("Operador"))
        {
            var services = _context.ServiceModel
                .Include(r => r.csscUser)
                .ToList();

            var data = new List<List<Services>>();
            foreach (var month in months)
            {
                var monthData = services.Where(s => s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM yyyy", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                                                    && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM yyyy", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                data.Add(monthData);
            }

            var viewModel = new CalendarViewModel() { Months = months, Data = data };
            return View(viewModel);
        }
        else if (User.IsInRole("Default"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userServices = await _context.ServiceModel
                .Include(r => r.csscUser)
                .Where(r => r.ServIdUtilizador == Guid.Parse(userId)).ToListAsync();

            var data = new List<List<Services>>();
            foreach (var month in months)
            {
                var monthData = userServices.Where(s => s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                                                        && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                data.Add(monthData);
            }

            var viewModel = new CalendarViewModel() { Months = months, Data = data };
            return View(viewModel);
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }

    /// <summary>
    /// Apresenta um calendário com base no intervalo de datas especificado.
    /// </summary>
    /// <param name="startDate">A data de início do calendário.</param>
    /// <param name="endDate">A data de término do calendário.</param>
    /// <returns>Um ActionResult representando a vista do calendário.</returns>
    [HttpPost]
    public async Task<ActionResult> Index(DateTime startDate, DateTime endDate)
    {
        var months = GetMonthsBetween(startDate, endDate);

        if (User.IsInRole("Operador"))
        {
            var services = _context.ServiceModel
                .Include(r => r.csscUser)
                .ToList();
            
            var data = new List<List<Services>>();
            foreach (var month in months)
            {
                var monthData = services.Where(s => s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM yyyy", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                                                    && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM yyyy", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                data.Add(monthData);
            }

            var viewModel = new CalendarViewModel() { Months = months, Data = data };
            return View(viewModel);
        }
        else if (User.IsInRole("Default"))
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userServices = await _context.ServiceModel
                .Include(r => r.csscUser)
                .Where(r => r.ServIdUtilizador == Guid.Parse(userId)).ToListAsync();
            
            var data = new List<List<Services>>();
            foreach (var month in months)
            {
                var monthData = userServices.Where(s => s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                                                    && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                data.Add(monthData);
            }

            var viewModel = new CalendarViewModel() { Months = months, Data = data };
            return View(viewModel);
        }
        else
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }

    /// <summary>
    /// Obtém uma lista de meses entre duas datas especificadas.
    /// </summary>
    /// <param name="startDate">A data de início.</param>
    /// <param name="endDate">A data de término.</param>
    /// <returns>Uma lista de meses no formato "MMMM yyyy".</returns>
    private List<string> GetMonthsBetween(DateTime startDate, DateTime endDate)
    {
        var months = new List<string>();
        while (startDate <= endDate)
        {
            months.Add(startDate.ToString("MMMM yyyy"));
            startDate = startDate.AddMonths(1);
        }
        return months;
    }
}
