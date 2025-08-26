using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseDAO _warehouseDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<WarehouseService> _logger;

        public WarehouseService(IWarehouseDAO warehouseDAO, IMapper mapper, ILogger<WarehouseService> logger)
        {
            _warehouseDAO = warehouseDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetWarehouse>> GetWarehousesAsync()
        {
            try
            {
                var warehouses = await _warehouseDAO.GetWarehousesAsync();
                return _mapper.Map<List<GetWarehouse>>(warehouses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving warehouses: {Message}", ex.Message);
                throw new Exception("Failed to retrieve warehouse datas: " + ex.Message);
            }
        }
    }
}
