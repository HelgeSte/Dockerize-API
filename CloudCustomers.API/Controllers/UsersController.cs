using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService; // need a builder to tell the framwork to use the IUsersService

    
    public UsersController(IUsersService usersService) {
        _usersService = usersService;
    }

    [HttpGet(Name = "GetUsers")] // Inversion of control by using dependenbcy injection
    public async Task<IActionResult> Get(){
        var users = await _usersService.GetAllUsers(); // this fixes the error in the second test (41:51++)

        if (users.Any())
        {
            return Ok(users);
        }
        return NotFound(); // Status code gets our first unit test to pass (Get_OnSuccess_ReturnsStatusCode200)
    }
}
