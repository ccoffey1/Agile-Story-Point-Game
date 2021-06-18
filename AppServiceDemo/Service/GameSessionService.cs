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
        Task<GameSession> GetByOwnerIdAsync(Guid playerId);
    }

    public class GameSessionService : IGameSessionService
    {
        private readonly ILogger<GameSessionService> _logger;
        private readonly IConfiguration _config;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IPlayerService _playerService;

        public GameSessionService(
            ILogger<GameSessionService> logger,
            IConfiguration config,
            IPlayerRepository playerRepository,
            IPlayerService playerService, 
            IGameSessionRepository gameSessionRepository)
        {
            _logger = logger;
            _config = config;
            _playerRepository = playerRepository;
            _playerService = playerService;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<string> CreateGameAsync(string playerName, string teamName)
        {
            _logger.LogInformation($"Attempting to create a game \"{teamName}\" owned by player \"{playerName}\"");

            bool playerExists = (await _playerRepository.GetByPlayerNameAsync(playerName)) != null;
            if (playerExists) throw new ArgumentException($"Invalid name \"{playerName}\". Player already exists.");

            Player player = null;

            await _gameSessionRepository.ExecuteInTransaction(async () =>
            {
                // create game and player
                var gameSession = await _gameSessionRepository.AddAsync(new GameSession()
                {
                    TeamName = teamName
                });

                player = await _playerRepository.AddAsync(new Player
                {
                    Name = playerName,
                    GameSessionId = gameSession.Id
                });

                // link player to game
                gameSession.OwnerPlayerId = player.Id;
                await _gameSessionRepository.UpdateAsync(gameSession);
            });

            return _playerService.GeneratePlayerJWT(new PlayerDto
            {
                Id = player.Id,
                PlayerName = player.Name,
                GameSessionId = player.GameSessionId
            });
        }

        public async Task<GameSession> GetByOwnerIdAsync(Guid playerId)
        {
            return await _gameSessionRepository.GetByOwnerIdAsync(playerId);
        }
    }
}
