using SendGrid;
using SendGrid.Helpers.Mail;
using System.Drawing;

namespace CSSC.CSSCServices
{
    /// <summary>
    /// Classe responsável por enviar e-mails utilizando o serviço SendGrid.
    /// </summary>
    public class EmailSender
    {
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


        private readonly ISendGridClient _client;
        

        public EmailSender()
        {
        }

        // Ajuste o construtor para aceitar ISendGridClient como dependência
        public EmailSender(ISendGridClient client)
        {
            _client = client;
        }

         
       
        public async Task<Response> SendEmail(string subject, string toEmail , string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("cssc.esa@gmail.com", "CSSC");
            var to = new EmailAddress(toEmail);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }

        public async Task<Response> SendEmailToOficina(string subject, string fromEmail, string message)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail);
            var to = new EmailAddress("cssc.esa@gmail.com", "CSSC");
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response;
        }

    }
}
