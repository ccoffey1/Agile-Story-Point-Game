using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserDto user)
        {
            IActionResult response = Unauthorized();

            string token = await _authenticationService.AuthenticateUserWithGameAsync(user);

            if (token != null)
            {
                response = Ok(new[] { token });
            }

            return response;
        }
    }
}
