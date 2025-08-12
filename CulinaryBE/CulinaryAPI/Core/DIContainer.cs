using ServiceObject.Configurations;

namespace CulinaryAPI.Core
{
    public static class DIContainer
    {
        public static void Configure(this IServiceCollection services)
        {
            services.ConfigureDAO();
            services.ConfigureService();
            services.ConfigureBackgroundService();
        }
    }
}
