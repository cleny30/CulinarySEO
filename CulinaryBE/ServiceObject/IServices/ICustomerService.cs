using BusinessObject.Models.Dto.Customer;

namespace ServiceObject.IServices
{
    public interface ICustomerService
    {
        Task UpdateCustomer(UpdateCustomerDto cusDto);
    }
}
