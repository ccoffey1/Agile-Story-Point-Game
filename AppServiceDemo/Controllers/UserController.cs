using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService authenticationService)
        {
            _userService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserDto user)
        {
            IActionResult response = Unauthorized();

            string token = await _userService.AuthenticateUserWithGameAsync(user);

            if (token != null)
            {
                response = Ok(new[] { token });
            }

            return response;
        }
    }
}
