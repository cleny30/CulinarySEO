using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface ICustomerService
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> AddNewCustomer(RegisterCustomerRequest model);
        Task<bool> UpdateCustomer(UpdateCustomerDto model);
    }
}
