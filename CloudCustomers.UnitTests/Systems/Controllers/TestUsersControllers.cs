using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class UnitTest1
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers);

        var sut = new UsersController(mockUsersService.Object); // Removes logger from controller, to make it simple at first
        // Act
        var result = (OkObjectResult)await sut.Get();

        // Assert - Make assertions about the particular arrangement and action
        result.StatusCode.Should().Be(200); // FluentAssertions.Should()
    }

    [Fact]
    public async Task GetOnSuccess_InvokeUserServiceExactlyOnce()
    {
        // Arrange
        var mockUserService = new Mock<IUsersService>(); //Mock<IUsersService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        // Act
        var result = await sut.Get();

        // Assert
        mockUserService.Verify(service => service.GetAllUsers(),// Use await for GetAllUsers in controller to make sure we run GetAllUsers method
            Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers);

        var sut = new UsersController(mockUsersService.Object);
        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>(); // works with null
        
    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        // Arrange
        var mockUsersService = new Mock<IUsersService>();

        mockUsersService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUsersService.Object);

        // Act
        var result = await sut.Get();

        // Assert 
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);

    }
    /* // Parameterized unit tests:
    [Theory] // parameterized unit tests
    [InlineData("foo", 1)] // run multiple times, or as many times
    [InlineData("bar", 1)] // that there are InlineData attributes
    public void Test2(string input, int bar) { 
        // ....
    }*/


}