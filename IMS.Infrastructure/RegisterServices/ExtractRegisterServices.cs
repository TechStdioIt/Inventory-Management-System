﻿using IMS.Application.ServiceInterface;
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
            services.AddScoped<IRole, RoleServices>();
            return services;
            
        }
    }
}
