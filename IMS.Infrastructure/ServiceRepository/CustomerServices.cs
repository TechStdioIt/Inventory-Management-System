﻿using Dapper;
using IMS.Application.ServiceInterface;
using IMS.Domain.ViewModels;
using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.ServiceRepository.BaseRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.ServiceRepository
{
    public class CustomerServices : BaseRepository<object>, ICustomer
    {
        public CustomerServices(IMSContextEF context, ApplicationDbContext applicationDb, IMSContextDapper contextDapper, UserManager<ApplicationDbUser> userManager, IConfiguration configuration) : base(context, applicationDb, contextDapper, userManager, configuration)
        {
        }

        public async Task<dynamic> CreateOrUpdateCustomer(CustomerVM data)
        {
            try
            {
                var parameters = new DynamicParameters();

                if (data.id == 0)
                {
                    parameters.Add("@FLAG", 1);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                    parameters.Add("@email", data.email);
                    parameters.Add("@phone", data.phone);
                    parameters.Add("@address", data.address);
                    parameters.Add("@customerTypeId", data.customerTypeId);
                    parameters.Add("@city", data.city);
                    parameters.Add("@cp", data.cp);

                }
                else
                {
                    parameters.Add("@FLAG", 1);
                    parameters.Add("@id", data.id);
                    parameters.Add("@userId", data.userId);
                    parameters.Add("@name", data.name);
                    parameters.Add("@email", data.email);
                    parameters.Add("@phone", data.phone);
                    parameters.Add("@address", data.address);
                    parameters.Add("@customerTypeId", data.customerTypeId);
                    parameters.Add("@city", data.city);
                    parameters.Add("@cp", data.cp);
                }
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Customer",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> DeleteCustomer(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 4);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Customer",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> GetAllCustomer()
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 3);
                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryAsync<dynamic>(
                        "SP_Customer",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<dynamic> GetCustomerById(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FLAG", 2);
                parameters.Add("@id", id);

                using (var _dp = _contextDapper.CreateConnection())
                {
                    var query = await _dp.QueryFirstOrDefaultAsync<dynamic>(
                        "SP_Category",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
