using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CSSC.Areas.Identity.Data;
using CSSC.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using CSSC.Controllers;

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
    public async Task<ActionResult> Index(DateTime startDate, DateTime endDate, bool agendadoCheckbox, bool concluidoCheckbox)
    {
        if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
        {
            TempData["IncompleteDates"] = true;
            return RedirectToAction("Index", "Calendar");
        }
        else
        {
            var months = GetMonthsBetween(startDate, endDate);
            var data = new List<List<Services>>();

            if (User.IsInRole("Operador"))
            {
                if (agendadoCheckbox == true && concluidoCheckbox == false)
                {
                    var services = _context.ServiceModel
                        .Include(r => r.csscUser)
                        .Where(s => s.EstadoDoServico == EstadoDoServico.Espera.ToString())
                        .ToList();
                    foreach (var month in months)
                    {
                        var monthData = services.Where(s =>
                            s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }
                }
                else if (concluidoCheckbox == true && agendadoCheckbox == false)
                {
                    var services = _context.ServiceModel
                        .Include(r => r.csscUser)
                        .Where(s => s.EstadoDoServico == EstadoDoServico.Entregue.ToString())
                        .ToList();
                    foreach (var month in months)
                    {
                        var monthData = services.Where(s =>
                            s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }
                }
                else
                {
                    var services = _context.ServiceModel
                        .Include(r => r.csscUser)
                        .ToList();
                    foreach (var month in months)
                    {
                        var monthData = services.Where(s =>
                            s.ServPrazo.Month == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime.ParseExact(month, "MMMM yyyy",
                                CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }
                }

                var viewModel = new CalendarViewModel() { Months = months, Data = data };
                return View(viewModel);
            }
            else if (User.IsInRole("Default"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (agendadoCheckbox == true && concluidoCheckbox == false)
                {
                    var userServices = await _context.ServiceModel
                        .Include(r => r.csscUser)
                        .Where(r => r.ServIdUtilizador == Guid.Parse(userId))
                        .Where(s => s.EstadoDoServico == EstadoDoServico.Espera.ToString()).ToListAsync();

                    foreach (var month in months)
                    {
                        var monthData = userServices.Where(s =>
                            s.ServPrazo.Month == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }

                }else if (concluidoCheckbox == true && agendadoCheckbox == false)
                {
                    var userServices = await _context.ServiceModel
                        .Include(r => r.csscUser)
                        .Where(r => r.ServIdUtilizador == Guid.Parse(userId))
                        .Where(s => s.EstadoDoServico == EstadoDoServico.Entregue.ToString()).ToListAsync();

                    foreach (var month in months)
                    {
                        var monthData = userServices.Where(s =>
                            s.ServPrazo.Month == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }
                }
                else
                {
                    var userServices = await _context.ServiceModel
                        .Include(r => r.csscUser)
                        .Where(r => r.ServIdUtilizador == Guid.Parse(userId)).ToListAsync();

                    foreach (var month in months)
                    {
                        var monthData = userServices.Where(s =>
                            s.ServPrazo.Month == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Month
                            && s.ServPrazo.Year == DateTime
                                .ParseExact(month, "MMMM", CultureInfo.CreateSpecificCulture("pt-PT")).Year).ToList();
                        data.Add(monthData);
                    }
                }

                var viewModel = new CalendarViewModel() { Months = months, Data = data };
                return View(viewModel);
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
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
