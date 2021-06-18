using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
    public interface IPlayerService
    {
       string GeneratePlayerJWT(PlayerDto playerDto);
       Task<PlayerDto> GetAsync(int id);
    }

    public class PlayerService : IPlayerService
    {
        private readonly ILogger<PlayerService> _logger;
        private readonly IConfiguration _config;
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameSessionRepository _gameSessionRepository;

        public PlayerService(
            ILogger<PlayerService> logger,
            IConfiguration config,
            IPlayerRepository playerRepository, 
            IGameSessionRepository gameSessionRepository)
        {
            _logger = logger;
            _config = config;
            _playerRepository = playerRepository;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<PlayerDto> GetAsync(int id)
        {
            _logger.LogInformation($"Fetching player by id {id}");

            var player = await _playerRepository.GetAsync(id);

            return new PlayerDto()
            {
                Id = player.Id,
                PlayerName = player.Name,
                GameSessionId = player.GameSessionId
            };
        }

        public string GeneratePlayerJWT(PlayerDto playerDto)
        {
            _logger.LogInformation("Generating JWT for player", playerDto);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, playerDto.Id.ToString()),
                    new Claim(ClaimTypes.Name, playerDto.PlayerName)
                    // TODO: Role?
                },
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
