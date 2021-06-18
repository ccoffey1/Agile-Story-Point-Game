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
    public interface IUserService
    {
       string GenerateUserJWT(UserDto userDto);
       Task<UserDto> GetAsync(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _config;
        private readonly IPlayerRepository _userRepository;
        private readonly IGameSessionRepository _gameSessionRepository;

        public UserService(
            ILogger<UserService> logger,
            IConfiguration config,
            IPlayerRepository userRepository, 
            IGameSessionRepository gameSessionRepository)
        {
            _logger = logger;
            _config = config;
            _userRepository = userRepository;
            _gameSessionRepository = gameSessionRepository;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return new UserDto()
            {
                Id = user.Id,
                PlayerName = user.PlayerName,
                GameSessionId = user.GameSessionId
            };
        }

        public string GenerateUserJWT(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDto.PlayerName)
                    // TODO: Role?
                },
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
