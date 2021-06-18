using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
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
        public async Task<IActionResult> Create(string playerName, string gameSessionName)
        {
            _logger.LogInformation($"Received request to create player {playerName} and game {gameSessionName}");
            var newGameResponse = await _gameSessionService.CreateGameAsync(playerName, gameSessionName);
            return Ok(newGameResponse);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join(string playerName, string joinCode)
        {
            _logger.LogInformation($"Received request to create player {playerName} and join game with code {joinCode}");
            string playerJwt = await _gameSessionService.JoinNewPlayerToGameAsync(playerName, joinCode);
            return Ok(playerJwt);
        }
    }
}
