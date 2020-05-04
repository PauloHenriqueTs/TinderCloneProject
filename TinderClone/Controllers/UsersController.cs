using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TinderClone.Modules.Users.Login;
using TinderClone.Modules.Users.Models;
using TinderClone.Modules.Users.Repository;
using TinderClone.Modules.Users.Service;
using TinderClone.Modules.Users.Shared;

namespace TinderClone.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> PostUser(UserDto model)
        {
            try
            {
                return await _userService.Login(model);
            }
            catch (Exception)
            {
                return BadRequest("Invalid User");
            }
        }

        [HttpPost("signup")]
        public async Task<ActionResult<User>> Signup(UserDto model)
        {
            try
            {
                return await _userService.Signup(model);
            }
            catch (Exception)
            {
                return BadRequest("Invalid User");
            }
        }
    }
}