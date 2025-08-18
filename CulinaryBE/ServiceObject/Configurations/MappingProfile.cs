using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Dto.Product;
using BusinessObject.Models.Entity;

namespace ServiceObject.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Account
            CreateMap<Manager, AccountData>();
            #endregion

            #region Product
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

            CreateMap<Product, ProductSummaryDto>();

            CreateMap<Product, ElasticProductDto>()
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
                        ))
                .ForMember(dest => dest.CategoryIds,
                       opt => opt.MapFrom(src => src.ProductCategoryMappings.Select(m => m.CategoryId)));
            #endregion

            #region Customer
            CreateMap<RegisterCustomerRequest, Customer>()
               .AfterMap((src, dest) =>
               {
                   dest.CustomerId = Guid.NewGuid();
               });

            CreateMap<Customer, AccountData>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(c => c.CustomerId))
                .AfterMap((src, dest) =>
                {
                    dest.RoleName = "Customer";
                });
            CreateMap<Customer, UpdateCustomerDto>();
            #endregion

            #region Cart
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src =>
                src.CartItems.Sum(ci =>
                    (ci.Product.Discount.HasValue
                        ? ci.Product.Price - (ci.Product.Price * ci.Product.Discount.Value / 100)
                        : ci.Product.Price) * ci.Quantity
                )
            ));

            CreateMap<CartItem, CartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src =>
                src.Product.Discount.HasValue
                    ? src.Product.Price - (src.Product.Price * src.Product.Discount.Value / 100)
                    : src.Product.Price
            ))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src =>
                src.Product.ProductImages
                    .FirstOrDefault(pi => pi.IsPrimary)!.ImageUrl
            ));


            #endregion

            #region Manager
            CreateMap<Manager, ManagerDto>();
            CreateMap<ManagerDto, Manager>();
            #endregion
        }
    }
}