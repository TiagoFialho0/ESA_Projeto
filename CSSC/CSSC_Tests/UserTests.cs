using CSSC.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using Moq;

namespace CSSC_Tests
{
    public class UserTests : IClassFixture<CSSCContextFixture>
    {
        private CSSCContext _context;

        public UserTests(CSSCContextFixture context)
        {
            _context = context.DbContext;
        }
        
        public UserManager<CSSCUser> setUpUserManager()
        {
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<CSSCUser>>();
            var validator = new Mock<IUserValidator<CSSCUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<CSSCUser>>();
            pwdValidators.Add(new PasswordValidator<CSSCUser>());
            var userManager = new UserManager<CSSCUser>(new UserStore<CSSCUser>(_context), options.Object, new PasswordHasher<CSSCUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<CSSCUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<CSSCUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            return userManager;
        }

        public SignInManager<CSSCUser> setUpSignInManager(UserManager<CSSCUser> userManager)
        {
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var mockContextAccessor = new Mock<IHttpContextAccessor>();
            var mockPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<CSSCUser>>();
            var signInManager = new SignInManager<CSSCUser>(userManager, mockContextAccessor.Object, mockPrincipalFactory.Object, options.Object,
                                                            new Mock<ILogger<SignInManager<CSSCUser>>>().Object,
                                                            new Mock<IAuthenticationSchemeProvider>().Object, new Mock<IUserConfirmation<CSSCUser>>().Object);

            return signInManager;
        }

        [Fact]
        public void Returns_True_When_Email_Is_Valid()
        {
            EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();

            Assert.True(emailAddressAttribute.IsValid("teste@teste.com"));
        }

        [Fact]
        public void Returns_False_When_Email_Is_Invalid()
        {
            EmailAddressAttribute emailAddressAttribute = new EmailAddressAttribute();

            Assert.False(emailAddressAttribute.IsValid("abcdefgh"));
        }

        [Fact]
        public async void Register_With_Valid_Password_Returns_Success()
        {
            // Arrange
            var userManager = setUpUserManager();
            var bookUsr = new CSSCUser() { UtDataDeNascimento = "01/01/1970", UtMorada = "Lisboa", UtNIF = 123456789 };

            // Act
            var result = await userManager.CreateAsync(bookUsr, "T€ste1");

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async void Register_With_Invalid_Password_Returns_Fail()
        {
            // Arrange
            var userManager = setUpUserManager();
            var bookUsr = new CSSCUser() { UtDataDeNascimento = "01/01/1970", UtMorada = "Lisboa", UtNIF = 123456789 };

            // Act
            var result = await userManager.CreateAsync(bookUsr, "teste");

            // Assert
            Assert.False(result.Succeeded);
        }
    }
}