using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServiceObject.IServices;
using System.IdentityModel.Tokens.Jwt;

namespace ServiceObject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IManagerDAO _managerDAO;
        private readonly ICustomerDAO _customerDAO;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private ILogger<AuthService> _logger;

        public AuthService(IManagerDAO managerDAO, ICustomerDAO customerDAO, IJwtService jwtService, IMapper mapper, ILogger<AuthService> logger)
        {
            _managerDAO = managerDAO;
            _customerDAO = customerDAO;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<LoginResponse> VerifyManager(LoginAccountModel loginAccountModel)
        {
            _logger.LogInformation("Verifying manager with email {Email}", loginAccountModel.Email);

            try
            {
                var accountData = await _managerDAO.VerifyAccountAsync(loginAccountModel);
                var accessToken = await _jwtService.GenerateJwtToken(accountData);
                var refreshToken = _jwtService.GenerateRefreshToken();
                var expiryDate = DateTime.UtcNow.AddDays(7);

                await _managerDAO.SaveRefreshTokenAsync(accountData.UserId, refreshToken, expiryDate);

                _logger.LogInformation("Login successful for email {Email}", loginAccountModel.Email);
                return new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = 15 * 60
                };
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Login failed for email {Email}", loginAccountModel.Email);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing login for email {Email}", loginAccountModel.Email);
                throw new ValidationException("Failed to process login request: " + ex.Message);
            }
        }

        public async Task<LoginResponse> VerifyCustomer(LoginAccountModel loginAccountModel)
        {
            _logger.LogInformation("Verifying customer with email {Email}", loginAccountModel.Email);

            try
            {
                var accountData = await _customerDAO.VerifyAccountAsync(loginAccountModel);
                var accessToken = await _jwtService.GenerateJwtToken(accountData);
                var refreshToken = _jwtService.GenerateRefreshToken();
                var expiryDate = DateTime.UtcNow.AddDays(7);

                await _customerDAO.SaveRefreshTokenAsync(accountData.UserId, refreshToken, expiryDate);

                _logger.LogInformation("Login successful for email {Email}", loginAccountModel.Email);
                return new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    ExpiresIn = 15 * 60
                };
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Login failed for email {Email}", loginAccountModel.Email);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing login for email {Email}", loginAccountModel.Email);
                throw new ValidationException("Failed to process login request: " + ex.Message);
            }
        }

        public async Task<LoginResponse> RefreshTokenManagerAsync(string accessToken, string refreshToken)
        {
            _logger.LogInformation("Refreshing token");

            try
            {
                var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
                var userId = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                    ?? throw new SecurityTokenException("Invalid access token"));

                var manager = await _managerDAO.GetRefreshTokenAsync(refreshToken)
                    ?? throw new SecurityTokenException("Invalid refresh token");

                if (manager.ManagerId != userId)
                {
                    throw new SecurityTokenException("Refresh token does not match user");
                }

                var accountData = new AccountData
                {
                    UserId = manager.ManagerId,
                    Username = manager.Username,
                    Email = manager.Email,
                    RoleName = manager.Role.RoleName,
                    Permissions = manager.Role.RolePermissions
                        .Select(rp => rp.Permission.PermissionName)
                        .ToList()
                };

                var newAccessToken = await _jwtService.GenerateJwtToken(accountData);
                var newRefreshToken = _jwtService.GenerateRefreshToken();
                var expiryDate = DateTime.UtcNow.AddDays(7);

                await _managerDAO.RevokeRefreshTokenAsync(refreshToken);
                await _managerDAO.SaveRefreshTokenAsync(userId, newRefreshToken, expiryDate);

                _logger.LogInformation("Token refreshed for user {UserId}", userId);
                return new LoginResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = 15 * 60 // 15 phút
                };
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "Invalid refresh token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                throw new ValidationException("Failed to refresh token: " + ex.Message);
            }
        }

        public async Task<LoginResponse> RefreshTokenCustomerAsync(string accessToken, string refreshToken)
        {
            _logger.LogInformation("Refreshing token");

            try
            {
                var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
                var userId = Guid.Parse(principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                    ?? throw new SecurityTokenException("Invalid access token"));

                var customer = await _customerDAO.GetRefreshTokenAsync(refreshToken)
                    ?? throw new SecurityTokenException("Invalid refresh token");

                if (customer.CustomerId != userId)
                {
                    throw new SecurityTokenException("Refresh token does not match user");
                }

                var accountData = new AccountData
                {
                    UserId = customer.CustomerId,
                    Username = "",
                    Email = customer.Email,
                    RoleName = "Customer",
                };

                var newAccessToken = await _jwtService.GenerateJwtToken(accountData);
                var newRefreshToken = _jwtService.GenerateRefreshToken();
                var expiryDate = DateTime.UtcNow.AddDays(7);

                await _customerDAO.RevokeRefreshTokenAsync(refreshToken);
                await _customerDAO.SaveRefreshTokenAsync(userId, newRefreshToken, expiryDate);

                _logger.LogInformation("Token refreshed for user {UserId}", userId);
                return new LoginResponse
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    ExpiresIn = 15 * 60 // 15 phút
                };
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogWarning(ex, "Invalid refresh token");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing token");
                throw new ValidationException("Failed to refresh token: " + ex.Message);
            }
        }
    }
}
