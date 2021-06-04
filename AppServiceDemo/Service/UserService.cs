using AppServiceDemo.Data.Contracts;
using AppServiceDemo.Data.Entities;
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
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserWithGameAsync(UserDto userDto);
    }

    public class UserService : IAuthenticationService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public UserService(
            ILogger<UserService> logger,
            IConfiguration config, 
            IUserRepository userRepository)
        {
            _logger = logger;
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateUserWithGameAsync(UserDto userDto)
        {
            // TODO: fetch game from gameSessionId, verify it exists
            // We will delete all users belonging to a game when said game exits?

            var user = new User()
            {
                GameSessionId = userDto.GameSessionId,
                FirstName = userDto.FirstName,
                CreatedAt = DateTime.Now
            };

            await _userRepository.AddAsync(user);

            userDto.Id = user.Id;

            return GenerateJSONWebToken(userDto);
        }

        private string GenerateJSONWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.FirstName)
                    // TODO: Role?
                },
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
