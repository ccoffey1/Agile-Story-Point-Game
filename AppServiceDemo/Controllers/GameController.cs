using AppServiceDemo.Data.Repository;
using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppServiceDemo.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly IGameSessionService _gameSessionService;

        public GameController(
            ILogger<GameController> logger,
            IGameSessionService gameSessionService, 
            IUserRepository userRepository)
        {
            _gameSessionService = gameSessionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string playerName, string teamName)
        {
            string userJwt = await _gameSessionService.CreateGameAsync(playerName, teamName);
            return Ok(userJwt);
        }
    }
}
