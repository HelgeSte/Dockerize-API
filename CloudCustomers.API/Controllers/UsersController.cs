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
        // when users has gotten all users, then the code contiues; first test failed because the code didn't call GetAllUsers().
        return Ok("All good"); // Status code gets our first unit test to pass (Get_OnSuccess_ReturnsStatusCode200)
    }

    // Inversion of control by using dependenbcy injection
    /* High-level modules should not depend on low-level modules. Both should dephend on abstractions.
     * 
     * Abstractions should not dephend on details. Details (i.e. implementations) should dephend on abstractions.
     */
}
