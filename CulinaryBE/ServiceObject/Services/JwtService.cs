using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceObject.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceObject.Services
{
    public class JwtService : IJwtService
    {
        private readonly ILogger<JwtService> _logger;
        private readonly IConfiguration _configuration;

        public JwtService(ILogger<JwtService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtToken(AccountData accountData)
        {
            try
            {

                var key = _configuration["Jwt:Key"];
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                int expiryMinutes = int.Parse(_configuration["Jwt:TokenExpiration"]);

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    _logger.LogError("JWT configuration is missing or invalid");
                    throw new ValidationException("JWT configuration is missing or invalid");
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, accountData.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, accountData.Username),
                    new Claim(JwtRegisteredClaimNames.Email, accountData.Email),
                    new Claim("Role", accountData.RoleName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var permission in accountData.Permissions)
                {
                    claims.Add(new Claim("Permission", permission));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                _logger.LogInformation("Successfully generated JWT for user {Email}", accountData.Email);
                return await Task.FromResult(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate JWT for user {Email}", accountData.Email);
                throw new ValidationException("Failed to generate JWT: " + ex.Message);
            }
        }
    }
}
