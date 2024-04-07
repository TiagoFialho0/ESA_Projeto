using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using CSSC.CSSCServices;
using CSSC.CSSCServices;
using CSSC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

    public class ServiceTests : IClassFixture<CSSCContextFixture>
    {
        private CSSCContext _context;

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

        public ServiceTests(CSSCContextFixture context)
        {
            _context = context.DbContext;
        }

        [Fact]
        public async Task ClassifyService()
        {
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["Success"] = "false";
            ServicesController _servicesController = new ServicesController(_context)
            {
                TempData = tempData
            };

            int serviceID = 500;
            int level = 4;
            string message = "This is a test message.";

            var result = await _servicesController.Classificacao(serviceID, level, message);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
