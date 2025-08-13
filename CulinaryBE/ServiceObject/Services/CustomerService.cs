using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAO _customerDAO;
        private readonly IMapper _mapper;
        private ILogger<AuthService> _logger;

        public CustomerService(ICustomerDAO customerDAO, IMapper mapper, ILogger<AuthService> logger)
        {
            _customerDAO = customerDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddNewCustomer(RegisterCustomerRequest model)
        {
            try
            {
                _logger.LogInformation("Processing add new customer");
                var customer = _mapper.Map<Customer>(model);
                var result = await _customerDAO.AddNewCustomer(customer);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding new customer: {@Model}", model);
                throw new ValidationException("Failed to add new customer: " + ex.Message);
            }
        }

        public async Task<bool> IsEmailExist(string email)
        {
            _logger.LogInformation("Checking if email exists in database: {Email}", email);

            try
            {
                return await _customerDAO.IsEmailExist(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing check for email: {Email}", email);
                throw new ValidationException("Failed to process check email: " + ex.Message);
            }
        }

        public async Task<bool> IsUsernameExist(string username)
        {
            _logger.LogInformation("Checking if username exists in database: {Username}", username);

            try
            {
                return await _customerDAO.IsUsernameExist(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing check for username: {Username}", username);
                throw new ValidationException("Failed to process check username: " + ex.Message);
            }
        }
    }
}
