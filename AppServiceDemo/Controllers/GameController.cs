using AppServiceDemo.Data.Contracts.Request;
using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly IGameSessionService _gameSessionService;

        public GameController(
            ILogger<GameController> logger,
            IGameSessionService gameSessionService)
        {
            _logger = logger;
            _gameSessionService = gameSessionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateGameSessionRequest request)
        {
            _logger.LogInformation($"Received request to create player {request.PlayerName} and game {request.GameSessionName}");
            var newGameResponse = await _gameSessionService.CreateGameAsync(request.PlayerName, request.GameSessionName);
            return Ok(newGameResponse);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join([FromBody]JoinGameSessionRequest request)
        {
            _logger.LogInformation($"Received request to create player {request.PlayerName} and join game with code {request.JoinCode}");
            var joinGameResponse = await _gameSessionService.JoinNewPlayerToGameAsync(request.PlayerName, request.JoinCode);
            return Ok(joinGameResponse);
        }

        [HttpGet("sessiondata")]
        public async Task<IActionResult> GetJoinedPlayers()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            int playerId = int.Parse(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            var gameSessionDataResponse = await _gameSessionService.GetGameSessionDataByOwnerId(playerId);

            if (gameSessionDataResponse == null)
            {
                return NotFound();
            }

            return Ok(gameSessionDataResponse);
        }
    }
}
