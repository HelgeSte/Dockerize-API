﻿using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            HttpClient httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            await sut.GetAllUsers();

            // Assert
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync", 
                    Times.Exactly(1), 
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
            );
            // Verify HTTP request is made!
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            // Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            HttpClient httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UsersService(httpClient,config);


            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            HttpClient httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(
                new UsersApiOptions
                {
                    Endpoint = endpoint
                });
            var sut = new UsersService(httpClient, config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }
    }
}
