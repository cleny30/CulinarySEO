using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Entity;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class VoucherDAO : IVoucherDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<VoucherDAO> _logger;

        public VoucherDAO(CulinaryContext context, ILogger<VoucherDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Voucher>> GetByCodesAsync(List<string> codes)
        {
            try
            {
                if (codes == null || !codes.Any()) return new List<Voucher>();

                var now = DateTime.UtcNow;

                var vouchers = await _context.Vouchers
                    .Where(v =>
                        codes.Contains(v.VoucherCode) &&
                        v.Status == VoucherStatus.Active &&
                        v.StartDate <= now &&
                        v.EndDate >= now &&
                        (v.UsageLimit == 0 || v.UsedCount < v.UsageLimit)
                    )
                    .ToListAsync();

                return vouchers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetByCodesAsync with codes: {Codes}", string.Join(",", codes));
                throw new DatabaseException("Failed to retrieve vouchers");
            }
        }
    }
}
