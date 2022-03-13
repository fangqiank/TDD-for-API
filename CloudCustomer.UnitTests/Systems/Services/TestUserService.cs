using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using CloudCustomer.API.Services;
using CloudCustomer.UnitTests.Fixtures;
using CloudCustomer.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomer.UnitTests.Systems.Services
{
    public class TestUserService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpRequest()
        {
            var expectedResponse = Userfixture.GetTestusers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/api/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            await sut.GetAllUsers();

            handlerMock.Protected().Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(x => x.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsers()
        {
            var expectedResponse = Userfixture.GetTestusers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/api/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            var result = await sut.GetAllUsers();

            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnNotFound()
        {
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();

            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/api/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            var result = await sut.GetAllUsers();

            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnListOfUsersOfExpectedSize()
        {
            var expectedResponse = Userfixture.GetTestusers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com/api/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });
            var sut = new UserService(httpClient, config);

            var result = await sut.GetAllUsers();

            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeConfiguredUrl()
        {
            var expectedResponse = Userfixture.GetTestusers();
            var endpoint = "https://example.com/api/users";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            }); 
            var sut = new UserService(httpClient, config);

            var result = await sut.GetAllUsers();

            result.Count.Should().Be(expectedResponse.Count);

            var uri = new Uri(endpoint);
            handlerMock
                .Protected()
                .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(x => 
                    x.Method == HttpMethod.Get 
                    && x.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
