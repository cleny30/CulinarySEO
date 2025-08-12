using AutoMapper;
using BusinessObject.Models.Dto.Customer;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAO customerDAO;
        private ILogger<CustomerService> _logger;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerDAO customerDAO, ILogger<CustomerService> logger, IMapper mapper)
        {
            this.customerDAO = customerDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task UpdateCustomer(UpdateCustomerDto cusDto)
        {
            try
            {
                var customer = _mapper.Map<Customer>(cusDto);
                await customerDAO.UpdateCustomerAsync(customer);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating customer");
                throw new Exception("Failed to update customer: " + ex.Message);
            }
        }
    }
}
