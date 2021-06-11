using AppServiceDemo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGameSessionService _gameSessionService;

        public UserController(IUserService authenticationService, IGameSessionService gameSessionService)
        {
            _userService = authenticationService;
            _gameSessionService = gameSessionService;
        }

        [HttpGet]
        [Authorize]
        [Route("ownedgame")]
        public async Task<IActionResult> GetOwnedGame()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid userId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var gameSession = await _gameSessionService.GetByOwnerIdAsync(userId);

            if (gameSession == null) 
                return NotFound();

            return Ok(gameSession.Id);
        }
    }
}
