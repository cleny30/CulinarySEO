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
        private ILogger<CustomerService> _logger;

        public CustomerService(ICustomerDAO customerDAO, IMapper mapper, ILogger<CustomerService> logger)
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

        public async Task<bool> ChangePassword(LoginAccountModel model)
        {
            _logger.LogInformation("Processing change password for customer with email: {Email}", model.Email);
            try
            {
                Customer? customer = await _customerDAO.GetCustomerByEmail(model.Email);
                return await _customerDAO.ChangePassword(customer, model.OldPassword, model.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for customer: {@Model}", model);
                throw new ValidationException("Failed to change password: " + ex.Message);
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

        public async Task<bool> UpdateCustomer(UpdateCustomerDto cusDto)
        {
            try
            {
                _logger.LogInformation("Processing update a customer");
                Customer? customerExist = await _customerDAO.GetCustomerByID(cusDto.CustomerId);
                if (customerExist == null)
                {
                    _logger.LogWarning("Customer with ID {CustomerId} does not exist", cusDto.CustomerId);
                    throw new ValidationException("Customer does not exist");
                }
                _mapper.Map(cusDto, customerExist);
                var result = await _customerDAO.UpdateCustomer(customerExist);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error udpating a customer: {@model}", cusDto);
                throw new ValidationException("Failed to update customer: " + ex.Message);
            }
        }
    }
}
