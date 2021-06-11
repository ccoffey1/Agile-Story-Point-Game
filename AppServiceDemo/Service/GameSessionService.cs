using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
    public interface IGameSessionService
    {
        Task<string> CreateGame(string playerName, string teamName);
    }

    public class GameSessionService : IGameSessionService
    {
        private readonly ILogger<GameSessionService> _logger;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public GameSessionService(
            ILogger<GameSessionService> logger,
            IConfiguration config,
            IUserRepository userRepository,
            IUserService userService)
        {
            _logger = logger;
            _config = config;
            _userRepository = userRepository;
            _userService = userService;
        }

        public Task<string> CreateGame(string playerName, string teamName)
        {
            // TODO: Create game
            // TODO: Create user tied to game - return JWT
            throw new NotImplementedException();
        }
    }
}
