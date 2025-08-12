using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Dto.Product;

namespace ServiceObject.IServices
{
    public interface IProductService
    {
        Task<List<GetProductDto>> GetAllProducts();
        Task<GetProductDetailDto> GetProductDetailById(Guid productId);

    }
}