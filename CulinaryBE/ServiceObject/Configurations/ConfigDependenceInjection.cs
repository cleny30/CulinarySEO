using DataAccess.DAOs;
using DataAccess.IDAOs;
using Microsoft.Extensions.DependencyInjection;
using ServiceObject.Background;
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
        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<ITokenSaveQueue, TokenSaveQueue>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
