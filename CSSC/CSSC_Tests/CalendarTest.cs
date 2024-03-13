using CSSC.Areas.Identity.Data;
using CSSC.Controllers;
using CSSC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace CSSC_Tests
{
    public class CalendarTest : IClassFixture<CSSCContextFixture>
    {
        private readonly Mock<UserManager<CSSCUser>> _userManagerMock;
        private readonly CSSCContext _dbContext;
        private readonly CalendarController _controller;

        public CalendarTest(CSSCContextFixture fixture)
        {
            _dbContext = fixture.DbContext; // Use the DbContext from the fixture


            var userStoreMock = new Mock<IUserStore<CSSCUser>>();
            _userManagerMock = new Mock<UserManager<CSSCUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);


            _controller = new CalendarController(_dbContext);


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "userId"),
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.Role, "Operador"),
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task Index_WhenUserIsOperador_ReturnsViewResultWithViewModel()
        {

            var result = await _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsAssignableFrom<CalendarViewModel>(viewResult.Model);
        }
    }
}
