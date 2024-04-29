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
using CSSC.CSSCServices;
using System.Net;
using Humanizer;
using SendGrid.Helpers.Mail.Model;
using SendGrid.Helpers.Mail;

namespace CSSC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly CSSCContext _context;

        /// <summary>
        /// Construtor do controlador ServicesController.
        /// </summary>
        /// <param name="context">Instância do CSSCContext para o contexto da base de dados</param>
        public ServicesController(CSSCContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Exibe a página inicial dos serviços com base no papel do utilizador.
        /// </summary>
        /// <returns>Um IActionResult representando a vista da página principal.</returns>
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Operador"))
            {
                var services = _context.ServiceModel
                    .Include(r => r.csscUser)
                    .Include(r => r.csscOperador)
                    .ToList();
                List<string> estados = typeof(EstadoDoServico).GetValuesWithDescriptions();
                ViewBag.Types = new SelectList(estados);
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
        /// Exibe detalhes de um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço a ser exibido.</param>
        /// <returns>Um IActionResult representando a vista de detalhes do serviço ou NotFound se o ID for nulo ou não existir.</returns>
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

            var servicesStates = await _context.ServicesStates.Where(m => m.ServIdServico == id).ToListAsync();

            // Passando a lista de ONGs para a ViewBag
            ViewBag.ServicesStates = servicesStates;

            return View(serviceModel);
        }

        /// <summary>
        /// Exibe a página de criação de um novo serviço.
        /// </summary>
        /// <returns>Um IActionResult representando a vista de criação de serviço.</returns>
        public IActionResult Create()
        {
            List<CSSCUser> users = _context.Users.ToList();
            List<Guid> ids = users.Select(user => Guid.Parse(user.Id)).ToList();
            ViewBag.Users = new SelectList(users, "Id", "Email");
            List<string> estados = typeof(EstadoDoServico).GetValuesWithDescriptions()
                .OrderByDescending(state => state == "Em espera") // Move "Em espera" to the front
                .ThenBy(state => state) // Sort the rest alphabetically
                .ToList();
            ViewBag.Types = new SelectList(estados);
            return View();
        }

        /// <summary>
        /// Processa o pedido de criação de um novo serviço.
        /// </summary>
        /// <param name="serviceModel">O modelo de serviço a ser criado.</param>
        /// <returns>Um IActionResult redirecionando para a página principal se a criação for bem-sucedida, ou a vista de criação com erros se houver problemas de validação.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServico,ServIdOperador,ServIdUtilizador,ServMarcaVeiculo,ServModeloVeiculo,ServMatriculaVeiculo,ServClassificacao,ServComentario,ServDataInicio,EstadoDoServico,DescricaoDoServico")] Services serviceModel)
        {
            if (ModelState.IsValid)
            {
                var userId = serviceModel.ServIdUtilizador.ToString();
                var csscUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                serviceModel.csscUser = csscUser;

                var operadorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                serviceModel.ServIdOperador = Guid.Parse(operadorId);

                var operadorUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == operadorId);
                serviceModel.csscOperador = operadorUser;

                _context.Add(serviceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceModel);
        }

        /// <summary>
        /// Exibe a página de edição de um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço a ser editado.</param>
        /// <returns>Um IActionResult representando a vista de edição do serviço ou NotFound se o ID for nulo ou o serviço não for encontrado.</returns>
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

        /// <summary>
        /// Processa o pedido de edição de um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço a ser editado.</param>
        /// <param name="serviceModel">O modelo de serviço com as alterações.</param>
        /// <returns>Um IActionResult redirecionando para a página principal se a edição for bem-sucedida, ou a vista de edição com erros se houver problemas de validação ou concorrência.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServico,ServIdUtilizador,ServMarcaVeiculo,ServModeloVeiculo,ServMatriculaVeiculo,ServClassificacao,ServComentario,ServDataInicio,EstadoDoServico,DescricaoDoServico")] Services serviceModel)
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

        /// <summary>
        /// Exibe a página de confirmação para apagar um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço a ser apagado.</param>
        /// <returns>Um IActionResult representando a vista de confirmação do apagamento do serviço ou NotFound se o ID for nulo ou o serviço não for encontrado.</returns>
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

        /// <summary>
        /// Confirma o apagamento de um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço a ser apagado.</param>
        /// <returns>Um IActionResult redirecionando para a página principal após o apagamento bem-sucedida.</returns>
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

        /// <summary>
        /// Exibe a página de agendamento de notificação para um serviço com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do serviço para o qual agendar a notificação.</param>
        /// <returns>Um IActionResult representando a vista de agendamento de notificação ou NotFound se o ID for nulo ou o serviço não for encontrado.</returns>
        public async Task<IActionResult> AgendarNotif(int? id)
        {
            if (id == null || _context.ServiceModel == null)
            {
                return NotFound();
            }

            var serviceModel = await _context.ServiceModel
                .FirstOrDefaultAsync(m => m.IdServico == id);

            var notificacaoModel = new CSSC.Models.Notificacao
            {
                IdServico = serviceModel.IdServico

            };
            if (serviceModel == null)
            {
                return NotFound();
            }

            return View(notificacaoModel);
        }

        /// <summary>
        /// Processa o formulário de agendamento de notificação.
        /// </summary>
        /// <param name="notificacao">O modelo de notificação a ser agendado.</param>
        /// <returns>Um IActionResult redirecionando para a página principal se o agendamento for bem-sucedido, ou a vista de agendamento com erros se houver problemas de validação.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgendarNotifForm([Bind("IdServico,DataInicial,IntervaloDeEnvio,TipoDeNotif")] Notificacao notificacao)
        {

            if (ModelState.IsValid)
            {
                var serviceModel = await _context.ServiceModel
               .FirstOrDefaultAsync(m => m.IdServico == notificacao.IdServico);


                _context.Add(notificacao);
                await _context.SaveChangesAsync();

                var userId = serviceModel.ServIdUtilizador.ToString();
                var csscUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var _destEmail = csscUser.Email;

                var marca = serviceModel.ServMarcaVeiculo.ToString();
                var campoMarca = await _context.ServiceModel.FirstOrDefaultAsync(u => u.ServMarcaVeiculo == marca);

                var modelo = serviceModel.ServModeloVeiculo.ToString();
                var campoModelo = await _context.ServiceModel.FirstOrDefaultAsync(u => u.ServModeloVeiculo == modelo);

                var matricula = serviceModel.ServMatriculaVeiculo.ToString();
                var campoMatricula = await _context.ServiceModel.FirstOrDefaultAsync(u => u.ServMarcaVeiculo == matricula);

                var data = serviceModel.ServDataInicio;
                var campoData = await _context.ServiceModel.FirstOrDefaultAsync(u => u.ServDataInicio == data);

                string _emailSubject = "Serviço de Reparação Agendado";
                var _message = "<h3>Foi agendado um serviço na sua oficina CarShopSolutions.</h3><br/>" +
                                "<h3>Dados do veiculo:</h3><b>Veículo:</b> " + marca + " " + modelo + "<br/><b>Matrícula:</b> " + matricula +
                                "<br/><br/><h3>Data do serviço: " + data.Date + "</h3>" +
                                "<br/><b>Clique <a href='https://carshopsolutionsandcontrol.azurewebsites.net/Services/Details/" + serviceModel.IdServico + "'>aqui</a> para consultar o estado do serviço.</b><br/>";

                //envia um mail de agendamento para o cliente
                EmailSender emailSender = new EmailSender();
                await emailSender.SendEmail(_emailSubject, _destEmail, _message);
                

                return RedirectToAction(nameof(Index));
            }
            return View(notificacao);
        }

        /// <summary>
        /// Atualiza o estado de um serviço com base no ID e no novo estado fornecidos.
        /// </summary>
        /// <param name="id">O ID do serviço a ser atualizado.</param>
        /// <param name="estadoDoServico">O novo estado do serviço.</param>
        /// <returns>Um IActionResult redirecionando para a página principal após a atualização bem-sucedida.</returns>
        [HttpPost]
        public async Task<IActionResult> AtualizarEstado(int id, string EstadoDoServico)
        {
            var serviceModel = await _context.ServiceModel.FindAsync(id);
            if (serviceModel == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(serviceModel);
            }

            serviceModel.EstadoDoServico = EstadoDoServico;

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

            //var servicesStates = await _context.ServicesStates.FindAsync(id);
            //if (servicesStates == null)
            //{
            //    return NotFound();
            //}

            //if (!ModelState.IsValid)
            //{
            //    return View(servicesStates);
            //}

            var servicesStates = CreateServicesStates();
            
            servicesStates.ServIdServico = id;
            servicesStates.EstadoDoServico = EstadoDoServico;
            servicesStates.alteracaoEstado = DateTime.Now;

            try
            {
                _context.Update(servicesStates);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesStatesExists(servicesStates.IdEstadoServico))
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

        /// <summary>
        /// Processa o contato com a oficina.
        /// </summary>
        /// <param name="idServico">O ID do serviço a ser agendado.</param>
        /// <param name="subject">O assunto do contato.</param>
        /// <param name="description">A descrição do contato.</param>
        /// <returns>
        /// Um <see cref="Task"/> que representa a operação assíncrona. Redireciona para a página de calendário se o contato for bem-sucedido.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactOficina(string idServico, string subject, string description)
        {

            if (ModelState.IsValid)
            {
                var serviceModel = await _context.ServiceModel
                    .FirstOrDefaultAsync(m => m.IdServico == int.Parse(idServico));

                var userId = serviceModel.ServIdUtilizador.ToString();
                var csscUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var fromUserEmail = csscUser.Email;
                var fromUserName = csscUser.UserName;

                //envia um mail de agendamento para o cliente
                EmailSender emailSender = new EmailSender();
                var responseEmail = await emailSender.SendEmailToOficina(serviceModel, subject, fromUserEmail, fromUserName, description);

                return RedirectToAction("Index", "Calendar");
            }
            return RedirectToAction("Index", "Calendar");
        }

        /// <summary>
        /// Retorna a visualização para a classificação de um serviço específico.
        /// </summary>
        /// <param name="id">O ID do serviço a ser classificado.</param>
        /// <returns>
        /// Um <see cref="Task"/> que representa a operação assíncrona. 
        /// Retorna a visualização para a classificação do serviço se o ID for válido e o serviço for encontrado.
        /// Retorna um resultado NotFound se o ID for nulo ou se o serviço não for encontrado.
        /// </returns>
        public async Task<IActionResult> Classificacao(int? id)
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

        /// <summary>
        /// Processa a classificação e os comentários para um serviço específico.
        /// </summary>
        /// <param name="id">O ID do serviço a ser classificado.</param>
        /// <param name="rating">A classificação atribuída ao serviço.</param>
        /// <param name="comments">Os comentários associados à classificação.</param>
        /// <returns>
        /// Um <see cref="Task"/> que representa a operação assíncrona. 
        /// Redireciona para a página inicial se a classificação for bem-sucedida.
        /// Retorna um resultado NotFound se o serviço não for encontrado.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Classificacao(int id, int rating, string comments)
        {
            // Encontrar o serviço pelo ID
            if(comments == null)
            {
                comments = "nada a adicionar";
            }
            TempData["Success"] = false;

            var serviceModel = await _context.ServiceModel.FirstOrDefaultAsync(m => m.IdServico == id);

            if (serviceModel == null)
            {
                return NotFound();
            }

            // Atualizar a classificação e os comentários
            serviceModel.ServClassificacao = rating;
            serviceModel.ServComentario = comments;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceModel);
                    await _context.SaveChangesAsync();

                    // Indicar o sucesso do envio
                    TempData["Success"] = true;
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
                return RedirectToAction("Index");
            }
            TempData["Failed"] = true;
            return RedirectToAction("Classificacao", new { id = serviceModel.IdServico });

        }


        /// <summary>
        /// Verifica se um serviço com o ID fornecido existe no contexto.
        /// </summary>
        /// <param name="id">O ID do serviço a ser verificado.</param>
        /// <returns>True se o serviço existir, False caso contrário.</returns>
        private bool ServiceModelExists(int id)
        {
          return (_context.ServiceModel?.Any(e => e.IdServico == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Verifica se existe um estado de serviço com o ID especificado.
        /// </summary>
        /// <param name="id">O ID do estado de serviço a ser verificado.</param>
        /// <returns>
        /// Retorna true se um estado de serviço com o ID especificado existe, caso contrário, retorna false.
        /// </returns>
        private bool ServicesStatesExists(int id)
        {
            return (_context.ServicesStates?.Any(e => e.IdEstadoServico == id)).GetValueOrDefault();
        }

        /// <summary>
        /// Cria uma nova instância de ServicesStates.
        /// </summary>
        /// <returns>
        /// Uma nova instância de ServicesStates.
        /// </returns>
        private ServicesStates CreateServicesStates()
        {
            try
            {
                return Activator.CreateInstance<ServicesStates>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ServicesStates)}'. ");
            }
        }
    }
}
