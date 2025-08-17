using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Npgsql;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ManagerDAO _managerDAO;
        private readonly IMapper _mapper;
        private ILogger<ManagerService> _logger;

        public ManagerService(ManagerDAO managerDAO, IMapper mapper, ILogger<ManagerService> logger)
        {
            _managerDAO = managerDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddManager(AddManagerDto addManagerDto)
        {
            try
            {
                Manager manager = _mapper.Map<Manager>(addManagerDto);
                if (await IsEmailExists(manager.Email))
                {
                    _logger.LogWarning("Email already exists: {Email}", manager.Email);
                    return false;
                }
                manager.Password = GeneratePasswordHash(addManagerDto.Password);
                await _managerDAO.AddManager(manager);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new manager: {@Model}", addManagerDto);
                throw new ValidationException("Failed to add new manager: " + ex.Message);
            }
        }

        private string GeneratePasswordHash(string password)
        {
            var hasher = new PasswordHasher<object>();
            string passwordHash = hasher.HashPassword(null, password);
            return passwordHash;
        }

        private async Task<bool> IsEmailExists(string email)
        {
            try
            {
                return await _managerDAO.IsEmailExist(email);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check if email exists: " + ex.Message);
            }
        }
    }
}
