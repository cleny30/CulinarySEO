using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Npgsql;
using ServiceObject.IServices;
using System.Globalization;

namespace ServiceObject.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerDAO _managerDAO;
        private readonly IMapper _mapper;
        private ILogger<ManagerService> _logger;

        public ManagerService(IManagerDAO managerDAO, IMapper mapper, ILogger<ManagerService> logger)
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
                if (await IsEmailExists(updateManagerDto.Email) && !await IsEmailOfAccount(updateManagerDto.Email, updateManagerDto.ManagerId))
                {
                    _logger.LogWarning("Email already exists: {Email}", updateManagerDto.Email);
                    return false;
                }
                var existingManager = await _managerDAO.GetManagerById(updateManagerDto.ManagerId);
                if (existingManager == null)
                {
                    _logger.LogWarning("Manager with ID {ManagerId} does not exist", updateManagerDto.ManagerId);
                    return false;
                }                               
                _mapper.Map(updateManagerDto, existingManager);
                existingManager.Password = GeneratePasswordHash(updateManagerDto.Password);
                return await _managerDAO.UpdateManager(existingManager);
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

        private async Task<bool> IsEmailOfAccount(string email, Guid guid)
        {
            try
            {
                return await _managerDAO.IsEmailOfAccount(email, guid);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check if email exists: " + ex.Message);
            }
        }
    }
}
