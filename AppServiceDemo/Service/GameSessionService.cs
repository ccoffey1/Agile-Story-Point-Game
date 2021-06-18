using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
    public interface IGameSessionService
    {
        Task<string> CreateGameAsync(string playerName, string teamName);
        Task<GameSession> GetByOwnerIdAsync(Guid userId);
    }

    public class GameSessionService : IGameSessionService
    {
        private readonly ILogger<GameSessionService> _logger;
        private readonly IConfiguration _config;
        private readonly IPlayerRepository _userRepository;
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IUserService _userService;

        public GameSessionService(
            ILogger<GameSessionService> logger,
            IConfiguration config,
            IPlayerRepository userRepository,
            IUserService userService, 
            IGameSessionRepository gameSessionRepository)
        {
            _logger = logger;
            _config = config;
            _userRepository = userRepository;
            _userService = userService;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<string> CreateGameAsync(string playerName, string teamName)
        {
            _logger.LogInformation($"Attempting to create a game \"{teamName}\" owned by \"{playerName}\"");

            bool userExists = (await _userRepository.GetByPlayerNameAsync(playerName)) != null;
            if (userExists) throw new ArgumentException($"Invalid name \"{playerName}\". Player already exists.");

            // create game and user
            var gameSession = await _gameSessionRepository.AddAsync(new GameSession()
            {
                TeamName =  teamName
            });

            var user = await _userRepository.AddAsync(new User
            {
                PlayerName = playerName,
                GameSessionId = gameSession.Id
            });

            // link user to game
            gameSession.OwnerUserId = user.Id;
            await _gameSessionRepository.UpdateAsync(gameSession);

            return _userService.GenerateUserJWT(new UserDto
            {
                Id = user.Id,
                PlayerName = user.PlayerName,
                GameSessionId = user.GameSessionId
            });
        }

        public async Task<GameSession> GetByOwnerIdAsync(Guid userId)
        {
            return await _gameSessionRepository.GetByOwnerIdAsync(userId);
        }
    }
}
