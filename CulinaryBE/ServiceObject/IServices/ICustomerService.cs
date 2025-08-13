using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface ICustomerService
    {
        Task<bool> UpdateCustomer(UpdateCustomerDto cusDto);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsUsernameExist(string username);
        Task<bool> AddNewCustomer(RegisterCustomerRequest model);
    }
}
