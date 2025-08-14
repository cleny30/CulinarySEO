using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace ServiceObject.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Manager, AccountData>();
            CreateMap<Product, GetProductDto>()
                .ForMember(dest => dest.FinalPrice,
                opt => opt.MapFrom(src =>
                    src.Discount.HasValue
                        ? src.Price - (src.Price * (src.Discount.Value / 100))
                        : src.Price
                ))
                .ForMember(dest => dest.CategoryName,
                        opt => opt.MapFrom(src => src.ProductCategoryMappings
                        .Select(p => p.Category)
                        .Select(pi => pi.CategoryName)
                        .ToList()))
            .ForMember(dest => dest.AverageRating,
                opt => opt.MapFrom(src =>
                    src.ProductReviews.Any(r => r.Rating.HasValue)
                        ? (decimal)src.ProductReviews.Where(r => r.Rating.HasValue).Average(r => r.Rating.Value)
                        : 0
                ))
            .ForMember(dest => dest.TotalQuantity,
                opt => opt.MapFrom(src =>
                    src.Stocks.Sum(s => s.Quantity)
                ))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom(src =>
                    src.ProductImages
                        .Select(img => img.ImageUrl)
                        .ToList()
                ));

            CreateMap<ProductReview, ProductReviewDto>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.FullName));

            CreateMap<Product, GetProductDetailDto>()
                .ForMember(dest => dest.CategoryName,
                        opt => opt.MapFrom(src => src.ProductCategoryMappings
                        .Select(p => p.Category)
                        .Select(pi => pi.CategoryName)
                        .ToList()))
                .ForMember(dest => dest.ProductImages,
                    opt => opt.MapFrom(src => src.ProductImages
                        .Select(pi => pi.ImageUrl)
                        .ToList()))
                .ForMember(dest => dest.Reviews,
                    opt => opt.MapFrom(src => src.ProductReviews))
                .ForMember(dest => dest.FinalPrice,
                    opt => opt.MapFrom(src =>
                        src.Discount.HasValue
                            ? src.Price - (src.Price * (src.Discount.Value / 100))
                            : src.Price
                    ));


            CreateMap<RegisterCustomerRequest, Customer>()
                .AfterMap((src, dest) =>
                {
                    dest.CustomerId = Guid.NewGuid();
                });

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ReviewCount, 
                opt => opt.MapFrom(src =>
                    src.ProductReviews.Count()
                ))
                .ForMember(dest => dest.AverageRating,
                    opt => opt.MapFrom(src =>
                        src.ProductReviews.Any(r => r.Rating.HasValue)
                            ? (decimal)src.ProductReviews.Where(r => r.Rating.HasValue).Average(r => r.Rating!.Value)
                            : 0
                    ))
                .ForMember(dest => dest.TotalQuantity,
                    opt => opt.MapFrom(src =>
                        src.Stocks.Sum(s => s.Quantity)
                    ))
                .ForMember(dest => dest.FinalPrice,
                    opt => opt.MapFrom(src =>
                        src.Discount.HasValue
                            ? src.Price - (src.Price * (src.Discount.Value / 100))
                            : src.Price
                    ))
                .ForMember(dest => dest.ProductImages,
                        opt => opt.MapFrom(src =>
                            src.ProductImages.Select(img => img.ImageUrl).ToList()
                        ));
            CreateMap<Category, CategoryForShop>();

        }
    }
}