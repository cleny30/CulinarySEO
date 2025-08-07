using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class AuthService : IAuthService
    {
        private readonly IManagerDAO _managerDAO;
        private ILogger<AuthService> _logger;
        private readonly IMapper _mapper;

        public AuthService(IManagerDAO managerDAO, ILogger<AuthService> logger, IMapper mapper)
        {
            _managerDAO = managerDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<string> GenerateJwtToken(AccountData accountData)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountData> VerifyManager(LoginAccountModel loginAccountModel)
        {
            try
            {
                var accountData = await _managerDAO.VerifyAccountAsync(loginAccountModel);
                if (accountData == null)
                {
                    _logger.LogWarning("Login failed for email {Email}: Invalid credentials", loginAccountModel.Email);
                    throw new NotFoundException("Invalid email or password");
                }

                return accountData;
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
    }
}
