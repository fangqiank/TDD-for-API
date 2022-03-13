using CloudCustomer.API.Controllers;
using CloudCustomer.API.Models;
using CloudCustomer.API.Services;
using CloudCustomer.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomer.UnitTests.Systems.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode_200()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers())
                .ReturnsAsync(Userfixture.GetTestusers());

            var sut = new UserController(mockUserService.Object);

            var result = (OkObjectResult)await sut.Get();

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokeUserService_Exactly_Once()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserService.Object);

            var result = await sut.Get();

            mockUserService.Verify(x => x.GetAllUsers(), Times.Once());
        } 

        [Fact]
        public async Task Get_OnSuccess_ReturnListOfUsers()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(Userfixture.GetTestusers());

            var sut = new UserController(mockUserService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task Get_OnNoUserFound_Return_404()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode_400()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new UserController(mockUserService.Object);

            var result = await sut.Get();

            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}