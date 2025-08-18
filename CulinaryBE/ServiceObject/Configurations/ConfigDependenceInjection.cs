using DataAccess.DAOs;
using DataAccess.IDAOs;
using Microsoft.Extensions.DependencyInjection;
using ServiceObject.Background.BackgroundServices;
using ServiceObject.Background.Queue;
using ServiceObject.IServices;
using ServiceObject.Services;

namespace ServiceObject.Configurations
{
    public static class ConfigDependenceInjection
    {
        public static void ConfigureDAO(this IServiceCollection services)
        {
            services.AddScoped<IManagerDAO, ManagerDAO>();
            services.AddScoped<ICustomerDAO, CustomerDAO>();
            services.AddScoped<IProductDAO, ProductDAO>();
            services.AddScoped<ICategoryDAO, CategoryDAO>();
            services.AddScoped<IPermissionDAO, PermissionDAO>();
            services.AddScoped<IOrderDAO, OrderDAO>();
            services.AddScoped<IRecommendationDAO, RecommendationDAO>();
            services.AddScoped<ICartDAO, CartDAO>();
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IRecommendationService, RecommendationService>();
            services.AddScoped<IElasticService, ElasticService>();
        }

        public static void ConfigureBackgroundService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenSaveQueue, TokenSaveQueue>();
            services.AddSingleton<ILogoutQueue, LogoutQueue>();
            services.AddSingleton<IEmailQueue, EmailQueue>();
            services.AddSingleton<IProductSyncQueue, ProductSyncQueue>();

            services.AddHostedService<TokenSaveBackgroundService>();
            services.AddHostedService<LogoutBackgroundService>();
            services.AddHostedService<EmailBackgroundService>();
            services.AddHostedService<ProductSyncBackgroundService>();
        }
    }
}
