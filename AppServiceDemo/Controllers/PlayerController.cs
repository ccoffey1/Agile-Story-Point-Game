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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IGameSessionService _gameSessionService;

        public PlayerController(IPlayerService authenticationService, IGameSessionService gameSessionService)
        {
            _playerService = authenticationService;
            _gameSessionService = gameSessionService;
        }

        [HttpGet]
        [Authorize]
        [Route("ownedgame")]
        public async Task<IActionResult> GetOwnedGame()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            Guid playerId = Guid.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);

            var gameSession = await _gameSessionService.GetByOwnerIdAsync(playerId);

            if (gameSession == null) 
                return NotFound();

            return Ok(gameSession.Id);
        }
    }
}
