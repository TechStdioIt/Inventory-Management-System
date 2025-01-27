using IMS.Application.ServiceInterface;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.ServiceRepository;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Infrastructure.RegisterServices
{
    public static class ExtractRegisterServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)

        {
            services.AddTransient<IMSContextDapper>();
            services.AddScoped<IAdministrator,AdministratorServices>();
            services.AddScoped<IStoreType, StoreTypeServices>();
            services.AddScoped<IIMSMenu, IMSMenuServices>();
            services.AddScoped<IRole,RoleServices>();
            services.AddScoped<ICategory,CategoryServices>();
            services.AddScoped<ISuppliers,SuppliersServices>();
            services.AddScoped<IBank, BankServices>();
            services.AddScoped<IUnit, UnitServices>();
            services.AddScoped<ICustomer, CustomerServices>();
            services.AddScoped<IWareHouse, WareHouseServices>();
            services.AddScoped<IOrderActivityStatus,OrderActivityStatusService>();
            services.AddScoped<IPurchaseType,PurchaseTypeServices>();
            services.AddScoped<IProduct,ProductService>();
            services.AddScoped<IPurchasOrder, PurchasOrderServices>();
            return services;
        }
    }
}
