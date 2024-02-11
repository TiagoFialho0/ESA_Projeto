using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSSC.Areas.Identity.Data;
using CSSC.Models;
using System.Security.Claims;
using System.Reflection;
using System.Runtime.Serialization;
using CSSC.Extensions;

namespace CSSC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly CSSCContext _context;

        public ServicesController(CSSCContext context)
        {
            _context = context;
        }

        // GET: ServiceModels
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

        // GET: ServiceModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServiceModel == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(serviceModel);
        }

        // GET: ServiceModels/Create
        public IActionResult Create()
        {
            List<CSSCUser> users = _context.Users.ToList();
            List<Guid> ids = users.Select(user => Guid.Parse(user.Id)).ToList();
            ViewBag.Users = new SelectList(users, "Id", "Email");
            List<string> estados = typeof(EstadoDoServico).GetValuesWithDescriptions();
            ViewBag.Types = new SelectList(estados);
            return View();
        }

        // POST: ServiceModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,ServIdUtilizador,ServMarcaVeiculo,ServModeloVeiculo,ServMatriculaVeiculo,ServClassificacao,ServComentario,ServPrazo,EstadoDoServico")] Services serviceModel)
        {
            if (ModelState.IsValid)
            {
                var userId = serviceModel.ServIdUtilizador.ToString();
                var csscUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                serviceModel.csscUser = csscUser;

                _context.Add(serviceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceModel);
        }

        // GET: ServiceModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServiceModel == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel.FindAsync(id);
            if (serviceModel == null)
            {
                return NotFound();
            }
            return View(serviceModel);
        }

        // POST: ServiceModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,ServUtilizador,ServMarcaVeiculo,ServModeloVeiculo,ServMatriculaVeiculo,ServClassificacao,ServComentario,ServPrazo,EstadoDoServico")] Services serviceModel)
        {
            if (id != serviceModel.IdServico)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceModelExists(serviceModel.IdServico))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(serviceModel);
        }

        // GET: ServiceModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServiceModel == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel
                .FirstOrDefaultAsync(m => m.IdServico == id);
            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(serviceModel);
        }

        // POST: ServiceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServiceModel == null)
            {
                return Problem("Entity set 'CSSCContext.ServiceModel'  is null.");
            }
            var serviceModel = await _context.ServiceModel.FindAsync(id);
            if (serviceModel != null)
            {
                _context.ServiceModel.Remove(serviceModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceModelExists(int id)
        {
          return (_context.ServiceModel?.Any(e => e.IdServico == id)).GetValueOrDefault();
        }
    }
}
