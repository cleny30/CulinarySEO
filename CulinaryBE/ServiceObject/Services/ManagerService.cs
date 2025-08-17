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

        public async Task<bool> AddManager(ManagerDto addManagerDto)
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

        public async Task<bool> DeleteManager(Guid managerId)
        {
            try
            {
                return await _managerDAO.DeleteManager(managerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting manager with ID: {ManagerId}", managerId);
                throw new ValidationException("Failed to delete manager: " + ex.Message);
            }
        }

        public async Task<ManagerDto?> GetManagerById(Guid managerId)
        {
            try
            {
                var result = await _managerDAO.GetManagerById(managerId);
                return _mapper.Map<ManagerDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving manager with ID: {ManagerId}", managerId);
                throw new ValidationException("Failed to retrieve manager: " + ex.Message);
            }
        }

        public async Task<IEnumerable<ManagerDto>> GetManagers()
        {
            try
            {
                var managers = await _managerDAO.GetManagers();
                return _mapper.Map<IEnumerable<ManagerDto>>(managers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving managers");
                throw new ValidationException("Failed to retrieve managers: " + ex.Message);
            }
        }

        public async Task<bool> UpdateManager(ManagerDto updateManagerDto)
        {
            try
            {
                if (await IsEmailExists(updateManagerDto.Email))
                {
                    _logger.LogWarning("Email already exists: {Email}", updateManagerDto.Email);
                    return false;
                }
                var manager = _mapper.Map<Manager>(updateManagerDto);
                return await _managerDAO.UpdateManager(manager);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating manager: {@Model}", updateManagerDto);
                throw new ValidationException("Failed to update manager: " + ex.Message);
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
