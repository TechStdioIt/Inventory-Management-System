using IMS.Infrastructure.DBContext;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Infrastructure.RegisterServices
{
    public static class ExtractRegisterServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)

        {
            services.AddTransient<IMSContextDapper>();
            return services;
        }
    }
}
