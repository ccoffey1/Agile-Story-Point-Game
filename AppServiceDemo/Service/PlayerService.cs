using AppServiceDemo.Data.Entities;
using AppServiceDemo.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceDemo.Service
{
    public interface IPlayerService
    {
       string GeneratePlayerJWT(Player player);
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

        public string GeneratePlayerJWT(Player player)
        {
            _logger.LogInformation("Generating JWT for player {PlayerName}", player.Name);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, player.Id.ToString()),
                    new Claim(ClaimTypes.Name, player.Name)
                    // TODO: Role?
                },
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
