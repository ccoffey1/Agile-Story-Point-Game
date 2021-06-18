using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
    public interface IGameSessionService
    {
        Task<string> CreateGameAsync(string playerName, string teamName);
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
                player = new Player
                {
                    Name = playerName
                };

                // create game and player
                var gameSession = await _gameSessionRepository.AddAsync(new GameSession()
                {
                    TeamName = teamName,
                    Players = new List<Player>() { player }
                });
            });

            return _playerService.GeneratePlayerJWT(new PlayerDto
            {
                Id = player.Id,
                PlayerName = player.Name,
                GameSessionId = player.GameSessionId
            });
        }
    }
}
