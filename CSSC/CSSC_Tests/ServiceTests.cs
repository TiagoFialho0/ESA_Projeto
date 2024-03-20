using CSSC.CSSCServices;
using CSSC.CSSCServices;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
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
            var toEmail = "tiagofialho2002@gmail.com";
            var message = "This is a test message.";


            var result = await service.SendEmail(subject, toEmail, message);
            Assert.True(result.IsSuccessStatusCode);

        }
    }
}
