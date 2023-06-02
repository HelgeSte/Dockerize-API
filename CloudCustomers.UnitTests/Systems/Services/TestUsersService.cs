using CloudCustomers.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            var sut = new UsersService();


            // Act
            await sut.GetAllUsers();

            // Assert
            // Verify HTTP request is made!
        }
    }
}
