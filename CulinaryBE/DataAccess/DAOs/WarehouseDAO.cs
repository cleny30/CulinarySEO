using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class WarehouseDAO : IWarehouseDAO
    {
        private readonly CulinaryContext _context;

        public WarehouseDAO(CulinaryContext context)
        {
            _context = context;
        }

        public async Task<Warehouse?> GetWarehouseByCityAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            return await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Location.ToLower() == city.ToLower());
        }
    }

}
