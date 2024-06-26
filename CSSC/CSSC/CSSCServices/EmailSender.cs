﻿using CSSC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Drawing;
using System.Windows;

namespace CSSC.CSSCServices
{
    /// <summary>
    /// Classe responsável por enviar e-mails utilizando o serviço SendGrid.
    /// </summary>
    /// 
    public class EmailSender
    {
        private const string API_KEY = "";
        private readonly ISendGridClient _client;

        public EmailSender()
        {
        }

        // Ajuste o construtor para aceitar ISendGridClient como dependência
        public EmailSender(ISendGridClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Envia um e-mail de forma assíncrona para o destinatário especificado.
        /// </summary>
        /// <param name="subject">O assunto do e-mail.</param>
        /// <param name="toEmail">O endereço de e-mail do destinatário.</param>
        /// <param name="message">A mensagem de texto puro que será enviada. Note que este método atualmente não suporta conteúdo HTML.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona de enviar um e-mail.</returns>
        /// <remarks>
        /// Esta função utiliza a API SendGrid para enviar e-mails. É necessário fornecer uma chave de API válida.
        /// Atualmente, o método envia e-mails com conteúdo de texto puro e não suporta conteúdo HTML.
        /// </remarks>
        /// <example>
        /// <code>
        /// var emailSender = new EmailSender();
        /// await emailSender.SendEmail("Teste de Assunto", "destinatario@example.com", "Este é um teste de mensagem.");
        /// </code>
        /// </example>
        ///
        public async Task<Response> SendEmail(string subject, string toEmail, string message)
        {
            var apiKey = API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cssc.esa@gmail.com", "CSSC");
            var to = new EmailAddress(toEmail);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlContent, plainTextContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }

        /// <summary>
        /// Envia um email para a oficina usando o serviço SendGrid.
        /// </summary>
        /// <param name="subject">O assunto do email.</param>
        /// <param name="fromEmail">O endereço de email do remetente.</param>
        /// <param name="fromUserName">O nome do remetente.</param>
        /// <param name="message">O corpo do email.</param>
        /// <returns>
        /// Um <see cref="Task"/> que representa a operação assíncrona. 
        /// Retorna um objeto <see cref="Response"/> contendo informações sobre o envio do email.
        /// </returns>
        public async Task<Response> SendEmailToOficina(Services serviceModel, string subject, string fromEmail, string fromUserName, string message)
        {
            string updatedSubject = subject + " (Email enviado a partir do website)";
            var apiKey = API_KEY;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail);
            from.Name = fromUserName;

            var to = new EmailAddress("cssc.esa@gmail.com", "CSSC");
            var plainTextContent = ""; // Deixe o conteúdo em branco para enviar apenas HTML
            var htmlContent = "<h3>Dados do cliente:</h3>" +
                              "<b>  Nome:</b> " + from.Name + "<br/><b>  Email:</b> " + from.Email + "<br/>" +
                              "<h3>Dados do veiculo:</h3>" +
                              "<b>  Veiculo:</b> " + serviceModel.ServMarcaVeiculo + " " + serviceModel.ServModeloVeiculo + "<br/><b>  Matricula:</b> " + serviceModel.ServMatriculaVeiculo +
                              "<br/><h3>  Conteúdo:</h3>" + message + "<br/><br/>";
            var msg = MailHelper.CreateSingleEmail(to, to, updatedSubject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            return response;
        }

    }
}
