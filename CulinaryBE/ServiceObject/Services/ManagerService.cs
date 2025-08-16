using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using BusinessObject.Models.Enum;
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

        public async Task<AddNewManagerStatus> AddManager(AddManagerDto addManagerDto)
        {
            try
            {
                Manager manager = _mapper.Map<Manager>(addManagerDto);
                if (await IsEmailExists(manager.Email))
                {
                    _logger.LogWarning("Email already exists: {Email}", manager.Email);
                    return AddNewManagerStatus.EmailAlreadyExists;
                }
                if (await IsPhoneExists(addManagerDto.Phone))
                {
                    _logger.LogWarning("Phone number already exists: {Phone}", addManagerDto.Phone);
                    return AddNewManagerStatus.PhoneAlreadyExists;
                }
                manager.Password = GeneratePasswordHash(addManagerDto.Password);
                return await _managerDAO.AddManager(manager) ? AddNewManagerStatus.Success : AddNewManagerStatus.DatabaseError;
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

        private async Task<bool> IsPhoneExists(string phone)
        {
            try
            {
                return await _managerDAO.IsPhoneExist(phone);
            }
            catch (NpgsqlException ex)
            {
                throw new DatabaseException("Failed to check if phone exists: " + ex.Message);
            }
        }
    }
}
