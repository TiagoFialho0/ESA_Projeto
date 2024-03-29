using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using CSSC.CSSCServices;
using CSSC.CSSCServices;
using CSSC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace CSSC_Tests
{

    public class ServiceTests
    {


        [Fact]
        public async Task SendEmail_CallsSendGridClient()
        {
            // Arrange

            var service = new EmailSender();
            var subject = "Test Subject";
            var toEmail = "cssc.esa@gmail.com";
            var message = "This is a test message.";


            var result = await service.SendEmail(subject, toEmail, message);
            Assert.True(result.IsSuccessStatusCode);

        }

        private readonly ServicesController _servicesController;

        public ServiceTests(ServicesController servicesController)
        {
            _servicesController = servicesController;
        }

        [Fact]
        public async Task ClassifyService()
        {
            var serviceID = 500;
            var level = 4;
            var message = "This is a test message.";

            var result = await _servicesController.Classificacao(serviceID, level, message);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Classificacao", redirectToActionResult.ActionName);
        }
    }
}
