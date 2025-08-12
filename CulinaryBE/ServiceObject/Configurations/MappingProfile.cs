using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Dto.Customer;
using BusinessObject.Models.Entity;

namespace ServiceObject.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Manager, AccountData>();
            CreateMap<Product, GetProductDto>()
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImageUrl).ToList()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<Customer, UpdateCustomerDto>();
        }
    }
}