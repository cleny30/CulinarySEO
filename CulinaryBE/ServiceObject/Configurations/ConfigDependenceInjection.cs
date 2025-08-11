using DataAccess.DAOs;
using DataAccess.IDAOs;
using Microsoft.Extensions.DependencyInjection;
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

        }

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
