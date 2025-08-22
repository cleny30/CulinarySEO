using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IVoucherDAO
    {
        Task<List<Voucher>> GetByCodesAsync(List<string> codes);

    }
}
