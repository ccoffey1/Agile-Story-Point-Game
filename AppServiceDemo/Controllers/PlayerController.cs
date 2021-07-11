using AppServiceDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace AppServiceDemo.Controllers
{
    [Route("api/[controller]")]
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
    }
}
