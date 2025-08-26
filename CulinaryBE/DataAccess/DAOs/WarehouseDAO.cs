using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class WarehouseDAO : IWarehouseDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<WarehouseDAO> _logger;

        public WarehouseDAO(CulinaryContext context, ILogger<WarehouseDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Warehouse?> GetWarehouseByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            return await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Location.ToLower() == city.ToLower());
        }

        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            try
            {
                return await _context.Warehouses
                    .Select(w => new Warehouse
                    {
                        WarehouseId = w.WarehouseId,
                        WarehouseName = w.WarehouseName,
                        Location = w.Location,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching warehouses.");
                throw;
            }
        }
    }

}
