using AppServiceDemo.Data.Repository;
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
        public async Task<IActionResult> Create(string playerName, string teamName)
        {

            string playerJwt = await _gameSessionService.CreateGameAsync(playerName, teamName);
            return Ok(playerJwt);
        }
    }
}
